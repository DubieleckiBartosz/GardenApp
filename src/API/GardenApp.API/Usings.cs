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
global using Offers.Application.Reference;
global using Offers.Infrastructure.Configurations;
global using Panels.Application;
global using Panels.Domain.Contractors.ValueTypes;
global using Panels.Infrastructure.Configurations;
global using Payments.Application.Interfaces.Services;
global using Payments.Application.Models.Parameters;
global using Payments.Application.Models.Responses;
global using Serilog;
global using Stripe;
global using Swashbuckle.AspNetCore.Annotations;
global using Users.Application.Configurations;
global using Users.Application.Handlers;
global using Users.Application.Reference;
global using Users.Infrastructure.Configurations;
global using Works.Application.Config;
global using Works.Application.Reference;
global using Works.Infrastructure.Configurations;
global using static Offers.Application.Handlers.AddGardenOfferItemHandler;
global using static Offers.Application.Handlers.CompleteOfferHandler;
global using static Offers.Application.Handlers.CreateGardenOfferHandler;
global using static Panels.Application.Handlers.Commands.AddLinkHandler;
global using static Panels.Application.Handlers.Commands.AddLogoHandler;
global using static Panels.Application.Handlers.Commands.CreateProjectHandler;
global using static Panels.Application.Handlers.Commands.RemoveLinkHandler;
global using static Panels.Application.Handlers.Commands.RemoveProjectHandler;
global using static Panels.Application.Handlers.Commands.UpdateProjectDescriptionHandler;
global using static Users.Application.Handlers.ConfirmUserHandler;
global using static Users.Application.Handlers.ForgotPasswordHandler;
global using static Users.Application.Handlers.LoginUserHandler;
global using static Users.Application.Handlers.RefreshTokenHandler;
global using static Users.Application.Handlers.RegisterBusinessHandler;
global using static Users.Application.Handlers.RegisterUserHandler;
global using static Users.Application.Handlers.ResetPasswordHandler;
global using static Users.Application.Handlers.RevokeTokenHandler;
global using static Users.Application.Integration.TestHandler;
global using Works.Application.Handlers.GardeningWork.Parameters;
global using Works.Application.Handlers.GardeningWork.Views;
global using Works.Application.Handlers.WorkItem.Parameters;
global using static Works.Application.Handlers.GardeningWork.AddGardeningWorkHandler;
global using static Works.Application.Handlers.GardeningWork.AddWorkItemHandler;
global using static Works.Application.Handlers.GardeningWork.GetGardeningWorkDetailsHandler;
global using static Works.Application.Handlers.GardeningWork.GetGardeningWorksHandler;
global using static Works.Application.Handlers.GardeningWork.UpdatePlannedEndDateHandler;
global using static Works.Application.Handlers.GardeningWork.UpdatePlannedStartDateHandler;
global using static Works.Application.Handlers.GardeningWork.UpdateStatusHandler;
global using static Works.Application.Handlers.WorkItem.AddTimeWeatherRecordHandler;
global using static Works.Application.Handlers.WorkItem.UpdateStatusHandler;
global using static Works.Application.Handlers.WorkItem.UpdateTimeWeatherRecordHandler;