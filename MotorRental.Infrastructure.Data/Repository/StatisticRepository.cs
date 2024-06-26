﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MotorRental.Entities;
using MotorRental.Infrastructure.Data;
using MotorRental.UseCase.Repository;
using MotorRental.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.Infrastructure.SqlServer.Repository
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly ApplicationDbContext _db;

        public StatisticRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<object> CalculateRentalsAndTotalAmount(string OwnerId, DateTime beginDate, DateTime endDate)
        {
            using var conn = new SqlConnection("Server=.;Database=MotorRental;Trusted_Connection=yes;MultipleActiveResultSets=true;TrustServerCertificate=Yes");
            conn.Open();

            var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT name,
                            LicensePlate,
                            COUNT(MotorbikeId) AS count,
                            COALESCE(SUM(Appointments.RentalPrice), 0) AS amount
                            FROM Motorbikes
                            LEFT JOIN Appointments
                            ON Motorbikes.Id = Appointments.MotorbikeId
                            WHERE UserId = '5585864b-7c6c-4a63-a98d-95dbea318ddb'
                            AND (RentalBegin BETWEEN '2024-05-10' AND '2024-06-20' OR RentalBegin IS NULL)
                            AND (Appointments.StatusAppointment != 2 OR Appointments.StatusAppointment IS NULL)
                            GROUP BY MotorbikeId, name, LicensePlate
                            ORDER BY count DESC";

            List<object> resultList = new List<object>();

            using var reader = cmd.ExecuteReader();
            {
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    string LicensePlate = reader.GetString(1);
                    int count = reader.GetInt32(2);
                    int amount = reader.GetInt32(3);

                 

                    resultList.Add(
                        new
                        {
                            Name = name,
                            LicensePlate = LicensePlate,
                            count = count,
                            amount = amount,
                        }
                    );
                }
                reader.Close();
            }

            reader.Close();
            conn.Close();

            return resultList;
        }

        public async Task<object> StatisticAmountAndCount(string OwnerId, DateTime beginDate, DateTime endDate)
        {
            var result = _db.Appointments
                .Where(a => a.OwnerId == OwnerId &&
                a.StatusAppointment != SD.Status_Appointment_Cancel &&
                            a.RentalBegin >= beginDate && a.RentalBegin <= endDate)
                .GroupBy(a => a.RentalBegin.Date)
                .Select(g => new
                {
                    day = g.Key.ToString("yyyy-MM-dd"),
                    count_rental = g.Count(),
                    amount = g.Sum(a => a.RentalPrice)
                });
            return await result.ToArrayAsync();

        }
    }
}
