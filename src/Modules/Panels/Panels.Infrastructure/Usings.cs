﻿global using BuildingBlocks.Application.Contracts.Integration;
global using BuildingBlocks.Application.Options;
global using BuildingBlocks.Application.Settings;
global using BuildingBlocks.Application.Tools;
global using BuildingBlocks.Application.Wrappers;
global using BuildingBlocks.Domain.Time;
global using BuildingBlocks.Domain.ValueObjects;
global using BuildingBlocks.Infrastructure.Config;
global using BuildingBlocks.Infrastructure.Database.Dapper;
global using BuildingBlocks.Infrastructure.Inbox;
global using MediatR;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Newtonsoft.Json;
global using Panels.Application;
global using Panels.Application.Handlers.Integration;
global using Panels.Application.Interfaces;
global using Panels.Domain.Contractors;
global using Panels.Domain.Contractors.Entities;
global using Panels.Domain.Contractors.ValueObjects;
global using Panels.Infrastructure.Database;
global using Panels.Infrastructure.Inbox;
global using Panels.Infrastructure.Integration;
global using Panels.Infrastructure.Processing;
global using Panels.Infrastructure.Repositories;
global using Serilog;
global using static Panels.Application.Handlers.Commands.CreateNewContractorHandler;