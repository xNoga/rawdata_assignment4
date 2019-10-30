# RAWDATA Assignment 4
This is an assignment for the RAWDATA course in Computer Science fall 2019 at Roskilde University.

## Creating the models
Entity Framework Core [reverse engineering](https://docs.microsoft.com/en-us/ef/core/managing-schemas/scaffolding) have been used to generate all models with the following command:

`dotnet ef dbcontext scaffold "Host=localhost;Database=postgres;Username=postgres;Password=*****" Npgsql.EntityFrameworkCore.PostgreSQL -o Models`