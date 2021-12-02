using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using Zadatak.Models;

namespace Zadatak.Dal
{
    class SqlRepositoryReservation : ISqlRepository
    {
        private const string HotelIdParam = "@hotelId";
        private const string PersonIdParam = "@personId";
        private const string FromDateParam = "@fromDate";
        private const string ToDateParam = "@toDate";
        private const string IdReservationParam = "@idReservation";

        private static readonly string cs = SqlRepository.cs;

        public void Add<T>(T item)
        {
            Reservation reservation = item as Reservation;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name + typeof(T).Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(HotelIdParam, reservation.HotelID);
                    cmd.Parameters.AddWithValue(PersonIdParam, reservation.PersonID);
                    cmd.Parameters.AddWithValue(FromDateParam, reservation.FromDate);
                    cmd.Parameters.AddWithValue(ToDateParam, reservation.ToDate);
                    SqlParameter idReservation = new SqlParameter(IdReservationParam, SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idReservation);
                    cmd.ExecuteNonQuery();
                    reservation.IDReservation = (int)idReservation.Value;
                }
            } 
        }

        public void Delete<T>(T item)
        {
            Reservation reservation = item as Reservation;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name + typeof(T).Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdReservationParam, reservation.IDReservation);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<T> GetList<T>()
        {
            IList<Reservation> reservations = new List<Reservation>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name + typeof(T).Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            reservations.Add(ReadReservation(dr) as Reservation);
                        }
                    }
                }
            }
            return (IList<T>)reservations;
        }

        public T Get<T>(int idReservation)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name + typeof(T).Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdReservationParam, idReservation);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return (T)ReadReservation(dr);
                        }
                    }
                }
            }
            throw new Exception("Hotel does not exist");
        }

        public static object ReadReservation(SqlDataReader dr) => new Reservation
        {
            IDReservation = (int)dr[nameof(Reservation.IDReservation)],
            HotelID = (int)dr[nameof(Reservation.HotelID)],
            PersonID = (int)dr[nameof(Reservation.PersonID)],
            FromDate = dr[nameof(Reservation.FromDate)].ToString(),
            ToDate = dr[nameof(Reservation.ToDate)].ToString()
        };

        public void Update<T>(T item)
        {
            Reservation reservation = item as Reservation;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name + typeof(T).Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(HotelIdParam, reservation.HotelID);
                    cmd.Parameters.AddWithValue(PersonIdParam, reservation.PersonID);
                    cmd.Parameters.AddWithValue(FromDateParam, reservation.FromDate);
                    cmd.Parameters.AddWithValue(ToDateParam, reservation.ToDate);
                    cmd.Parameters.AddWithValue(IdReservationParam, reservation.IDReservation);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
