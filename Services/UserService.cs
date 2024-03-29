﻿namespace TP_Complot_Rest.Services;

using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TP_Complot_Rest.Common;
using TP_Complot_Rest.Common.Helpers;
using TP_Complot_Rest.Common.Models.Authentification;
using TP_Complot_Rest.Persistence.Entities;
using TP_Complot_Rest.Repositories;

public class UserService
{
    private readonly UserRepository _repo;

    private readonly AppSettings _appSettings;
    private readonly IMapper _mapper;

    public UserService(IOptions<AppSettings> appSettings, IMapper mapper, UserRepository repo)
    {
        _appSettings = appSettings.Value;
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Result<AuthenticateResponse>> Authenticate(AuthenticateRequest model)

    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            model.Password = GetHash(sha256Hash, model.Password);
        }
        User user = await _repo.FindUser(model.Email, model.Password);
        if (user == null)
            return Result.Failure<AuthenticateResponse>("Could not find User wiht given credentials");

        string token = generateJwtToken(user);
        if (token == null)
            return Result.Failure<AuthenticateResponse>("Could not generate User token");
        user.Token = token;
        AuthenticateResponse response = _mapper.Map<AuthenticateResponse>(user);
        if (response == null)
            return Result.Failure<AuthenticateResponse>("Could not map user into Authenticate response");
        return Result.Success(response);
    }

    private string generateJwtToken(User user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("email", user.Email.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private static string GetHash(HashAlgorithm hashAlgorithm, string input)
    {
        // Convert the input string to a byte array and compute the hash.
        byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        var sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }

    private static bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
    {
        var hashOfInput = GetHash(hashAlgorithm, input);

        // Create a StringComparer an compare the hashes.
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        return comparer.Compare(hashOfInput, hash) == 0;
    }

    public async Task<Result<AuthenticateResponse>> Register(CreateUserRequest model)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            model.Password = GetHash(sha256Hash, model.Password);
        }
        User user = await _repo.FindUser(model.Email, model.Password);

        User toCreate = _mapper.Map<User>(model);
        if (toCreate == null)
            return Result.Failure<AuthenticateResponse>("Could not map the given user into an entity");

        if (user != null)
            return Result.Failure<AuthenticateResponse>("User already exist with this email");
        string token = generateJwtToken(toCreate);
        if (token == null)
            return Result.Failure<AuthenticateResponse>("Could not generate User token");
        toCreate.Token = token;
        await _repo.Create(toCreate);
        AuthenticateResponse response = _mapper.Map<AuthenticateResponse>(toCreate);
        if (response == null)
            return Result.Failure<AuthenticateResponse>("Could not map user into Authenticate response");
        return Result.Success(response);
    }

    public Result<AuthenticateResponse> Get(string email)
    {
        User user = _repo.FindByEmail(email).Result;
        if (user == null)
            return Result.Failure<AuthenticateResponse>("Could not find user");
        AuthenticateResponse response = _mapper.Map<AuthenticateResponse>(user);
        if (response == null)
            return Result.Failure<AuthenticateResponse>("Could not map user into Authenticate response");
        return Result.Success(response);
    }
}