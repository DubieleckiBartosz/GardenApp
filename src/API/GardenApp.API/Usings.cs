﻿global using BuildingBlocks.Application.Config;
global using BuildingBlocks.Application.Contracts.Mediator;
global using BuildingBlocks.Application.Exceptions;
global using BuildingBlocks.Application.Wrappers;
global using BuildingBlocks.Domain.Exceptions;
global using BuildingBlocks.Infrastructure.Config;
global using GardenApp.API.Common;
global using GardenApp.API.Configurations;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.OpenApi.Models;
global using Newtonsoft.Json;
global using Offers.Infrastructure.Configurations;
global using Panels.Application;
global using Panels.Infrastructure.Configurations;
global using Serilog;
global using Swashbuckle.AspNetCore.Annotations;
global using Users.Application.Configurations;
global using Users.Application.Handlers;
global using Users.Application.Reference;
global using Users.Infrastructure.Configurations;
global using static Offers.Application.Handlers.AddGardenOfferItemHandler;
global using static Offers.Application.Handlers.CompleteOfferHandler;
global using static Offers.Application.Handlers.CreateGardenOfferHandler;
global using static Users.Application.Handlers.ConfirmUserHandler;
global using static Users.Application.Handlers.LoginUserHandler;
global using static Users.Application.Handlers.RefreshTokenHandler;
global using static Users.Application.Integration.TestHandler;
global using static Users.Application.Handlers.ForgotPasswordHandler;
global using static Users.Application.Handlers.RegisterBusinessHandler;
global using static Users.Application.Handlers.RegisterUserHandler;
global using static Users.Application.Handlers.ResetPasswordHandler;
global using static Users.Application.Handlers.RevokeTokenHandler;