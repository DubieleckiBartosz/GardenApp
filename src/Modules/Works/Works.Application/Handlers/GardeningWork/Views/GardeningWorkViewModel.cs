﻿namespace Works.Application.Handlers.GardeningWork.Views;

public class GardeningWorkViewModel
{
    public int Id { get; }
    public string ClientEmail { get; }
    public int Priority { get; }
    public DateTime PlannedStartDate { get; }
    public int Status { get; }
    public string City { get; }
    public string Street { get; }
    public string NumberStreet { get; }
    private IEnumerable<GardeningWorkTagViewModel>? Tags { get; }

    protected GardeningWorkViewModel(
        int id,
        string clientEmail,
        int priority,
        DateTime plannedStartDate,
        int status,
        string city,
        string street,
        string numberStreet,
        IEnumerable<GardeningWorkTagViewModel>? tags)
    {
        Id = id;
        ClientEmail = clientEmail;
        Priority = priority;
        PlannedStartDate = plannedStartDate;
        Status = status;
        City = city;
        Street = street;
        NumberStreet = numberStreet;
        Tags = tags;
    }

    public static implicit operator GardeningWorkViewModel(GardeningWorkDao gardeningWork)
    {
        var tags = gardeningWork.Tags?.Select(_ =>
        {
            GardeningWorkTagViewModel tag = _;
            return tag;
        });

        return new GardeningWorkViewModel(
            gardeningWork.Id,
            gardeningWork.ClientEmail,
            gardeningWork.Priority,
            gardeningWork.PlannedStartDate,
            gardeningWork.Status,
            gardeningWork.City,
            gardeningWork.Street,
            gardeningWork.NumberStreet, tags);
    }
}