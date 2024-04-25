﻿global using BuildingBlocks.Application.Contracts.Mediator;
global using BuildingBlocks.Application.Contracts.Repositories;
global using BuildingBlocks.Application.Contracts.Services;
global using BuildingBlocks.Application.Exceptions;
global using BuildingBlocks.Application.Tools;
global using BuildingBlocks.Application.Wrappers;
global using BuildingBlocks.Domain.Entities;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.DependencyInjection;
global using Newtonsoft.Json;
global using Works.Application.Constants;
global using Works.Application.Handlers.GardeningWork.Parameters;
global using Works.Application.Handlers.GardeningWork.Views;
global using Works.Application.Handlers.WorkItem.Parameters;
global using Works.Application.Helpers;
global using Works.Application.Interfaces.Clients;
global using Works.Application.Interfaces.Repositories;
global using Works.Application.Interfaces.Services;
global using Works.Application.Models.DataAccess;
global using Works.Application.Models.Enums;
global using Works.Application.Models.Geographic;
global using Works.Application.Models.Weather.Actual;
global using Works.Application.Models.Weather.Forecast;
global using Works.Application.Models.Weather.History;
global using Works.Application.Services;
global using Works.Domain.GardeningWorks;
global using Works.Domain.GardeningWorks.ValueObjects;
global using Works.Domain.GardeningWorks.ValueTypes;
global using Works.Domain.WorkItems;
global using Works.Domain.WorkItems.ValueObjects;
global using Works.Domain.WorkItems.ValueTypes;
global using static Works.Application.Handlers.GardeningWork.AddGardeningWorkHandler;
global using static Works.Application.Handlers.GardeningWork.AddWorkItemHandler;
global using static Works.Application.Handlers.GardeningWork.GetGardeningWorksHandler;
global using static Works.Application.Handlers.GardeningWork.UpdatePlannedEndDateHandler;
global using static Works.Application.Handlers.GardeningWork.UpdatePlannedStartDateHandler;
global using static Works.Application.Handlers.GardeningWork.UpdateStatusHandler;
global using static Works.Application.Handlers.WorkItem.AddTimeWeatherRecordHandler;
global using static Works.Application.Handlers.WorkItem.UpdateStatusHandler;
global using static Works.Application.Handlers.WorkItem.UpdateTimeWeatherRecordHandler;
global using static Works.Application.Handlers.GardeningWork.GetGardeningWorkDetailsHandler;