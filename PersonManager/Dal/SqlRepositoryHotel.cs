using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Zadatak.Models;

namespace Zadatak.Dal
{
    class SqlRepositoryHotel : ISqlRepository
    {
        private const string AddressParam = "@address";
        private const string CityParam = "@city";
        private const string StarsParam = "@stars";
        private const string IdHotelParam = "@idHotel";
        private const string GetReservations = "GetReservationsHotel";
        private const string ReservationIdParam = "@idHotel";

        private static readonly string cs = SqlRepository.cs;

        public void Add<T>(T item)
        {
            Hotel hotel = item as Hotel;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name + typeof(T).Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(AddressParam, hotel.Address);
                    cmd.Parameters.AddWithValue(CityParam, hotel.City);
                    cmd.Parameters.AddWithValue(StarsParam, hotel.Stars);
                    SqlParameter idHotel = new SqlParameter(IdHotelParam, SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idHotel);
                    cmd.ExecuteNonQuery();
                    hotel.IDHotel = (int)idHotel.Value;
                }
            }
        }

        public void Delete<T>(T item)
        {
            Hotel hotel = item as Hotel;
            foreach (var reservation in hotel.Reservation)
            {
                RepositoryFactory.GetRepository().Delete(reservation);
            }
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name + typeof(T).Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdHotelParam, hotel.IDHotel);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<T> GetList<T>()
        {
            IList<Hotel> hotels = new List<Hotel>();
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
                            Hotel hotel = ReadHotel(dr) as Hotel;
                            using (SqlConnection con1 = new SqlConnection(cs))
                            {
                                con1.Open();
                                using (SqlCommand cmd1 = con1.CreateCommand())
                                {
                                    cmd1.CommandText = GetReservations;
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue(ReservationIdParam, hotel.IDHotel);
                                    using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                    {
                                        while (dr1.Read())
                                        {
                                            hotel.Reservation.Add(SqlRepositoryReservation.ReadReservation(dr1) as Reservation);
                                        }
                                    }
                                    hotels.Add(hotel);
                                }
                            }
                        }
                    }
                }
            }
            return (IList<T>)hotels;
        }

        public T Get<T>(int idHotel)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name + typeof(T).Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdHotelParam, idHotel);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Hotel hotel = ReadHotel(dr) as Hotel;
                            using (SqlConnection con1 = new SqlConnection(cs))
                            {
                                con1.Open();
                                using (SqlCommand cmd1 = con1.CreateCommand())
                                {
                                    cmd1.CommandText = GetReservations;
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue(ReservationIdParam, hotel.IDHotel);
                                    using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                    {
                                        while (dr1.Read())
                                        {
                                            hotel.Reservation.Add(SqlRepositoryReservation.ReadReservation(dr1) as Reservation);
                                        }
                                    }
                                }
                            }
                            return (T)(hotel as object);
                        }
                    }
                }
            }
            throw new Exception("Hotel does not exist");
        }

        public static object ReadHotel(SqlDataReader dr) => new Hotel
        {
            IDHotel = (int)dr[nameof(Hotel.IDHotel)],
            Address = dr[nameof(Hotel.Address)].ToString(),
            City = dr[nameof(Hotel.City)].ToString(),
            Stars = (int)dr[nameof(Hotel.Stars)]
        };

        public void Update<T>(T item)
        {
            Hotel hotel = item as Hotel;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name + typeof(T).Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(AddressParam, hotel.Address);
                    cmd.Parameters.AddWithValue(CityParam, hotel.City);
                    cmd.Parameters.AddWithValue(StarsParam, hotel.Stars);
                    cmd.Parameters.AddWithValue(IdHotelParam, hotel.IDHotel);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
