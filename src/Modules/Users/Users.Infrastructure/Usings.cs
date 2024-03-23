﻿global using BuildingBlocks.Application.Contracts.Integration;
global using BuildingBlocks.Application.Contracts.Repositories;
global using BuildingBlocks.Application.Options;
global using BuildingBlocks.Application.Wrappers;
global using BuildingBlocks.Domain.Time;
global using BuildingBlocks.Domain.Types;
global using BuildingBlocks.Infrastructure.Config;
global using BuildingBlocks.Infrastructure.Database.EntityFramework;
global using BuildingBlocks.Infrastructure.Module;
global using BuildingBlocks.Infrastructure.Outbox;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.IdentityModel.Tokens;
global using Newtonsoft.Json;
global using Serilog;
global using System.Linq.Expressions;
global using System.Text;
global using Users.Application.Enums;
global using Users.Application.Interfaces;
global using Users.Application.Options;
global using Users.Application.Settings;
global using Users.Domain.Users;
global using Users.Infrastructure.Database;
global using Users.Infrastructure.Database.Domain;
global using Users.Infrastructure.Database.Seed;
global using Users.Infrastructure.Outbox;
global using Users.Infrastructure.Repositories;