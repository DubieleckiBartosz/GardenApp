﻿global using BuildingBlocks.Application.Contracts.Repositories;
global using BuildingBlocks.Application.Options;
global using BuildingBlocks.Domain.Entities;
global using BuildingBlocks.Domain.Time;
global using BuildingBlocks.Domain.ValueObjects;
global using BuildingBlocks.Infrastructure.Config;
global using BuildingBlocks.Infrastructure.Database.Dapper;
global using BuildingBlocks.Infrastructure.Database.EntityFramework.Extensions;
global using Dapper;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Offers.Application.Contracts;
global using Offers.Application.Models.DataAccess;
global using Offers.Domain;
global using Offers.Domain.ValueTypes;
global using Offers.Infrastructure.Database;
global using Offers.Infrastructure.Database.Configurations;
global using Offers.Infrastructure.Database.Configurations.Converters;
global using Offers.Infrastructure.Repositories;
global using Serilog;