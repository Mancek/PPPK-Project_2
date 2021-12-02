using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Zadatak.Models;
using Zadatak.Utils;

namespace Zadatak.Dal
{
    class SqlRepositoryPerson : ISqlRepository
    {
        private const string FirstNameParam = "@firstname";
        private const string LastNameParam = "@lastname";
        private const string AgeParam = "@age";
        private const string EmailParam = "@email";
        private const string PictureParam = "@picture";
        private const string IdPersonParam = "@idPerson";
        private const string GetReservations = "GetReservationsPerson";
        private const string ReservationIdParam = "@idPerson";

        private static readonly string cs = SqlRepository.cs;

        public void Add<T>(T item)
        {
            Person person = item as Person;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name + typeof(T).Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(FirstNameParam, person.FirstName);
                    cmd.Parameters.AddWithValue(LastNameParam, person.LastName);
                    cmd.Parameters.AddWithValue(AgeParam, person.Age);
                    cmd.Parameters.AddWithValue(EmailParam, person.Email);
                    cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, person.Picture.Length)
                    {
                        Value = person.Picture
                    });
                    SqlParameter idPerson = new SqlParameter(IdPersonParam, SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(idPerson);
                    cmd.ExecuteNonQuery();
                    person.IDPerson = (int)idPerson.Value;
                }
            }
        }

        public void Delete<T>(T item)
        {
            Person person = item as Person;
            foreach (var reservation in person.Reservation)
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
                    cmd.Parameters.AddWithValue(IdPersonParam, person.IDPerson);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IList<T> GetList<T>()
        {
            IList<Person> people = new List<Person>();
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
                            Person person = ReadPerson(dr) as Person;
                            using (SqlConnection con1 = new SqlConnection(cs))
                            {
                                con1.Open();
                                using (SqlCommand cmd1 = con1.CreateCommand())
                                {
                                    cmd1.CommandText = GetReservations;
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue(ReservationIdParam, person.IDPerson);
                                    using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                    {
                                        while (dr1.Read())
                                        {
                                            person.Reservation.Add(SqlRepositoryReservation.ReadReservation(dr1) as Reservation);
                                        }
                                    }
                                    people.Add(person);
                                }
                            }
                        }
                    }
                }
            }
            return (IList<T>)people;
        }

        public T Get<T>(int idPerson)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name + typeof(T).Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(IdPersonParam, idPerson);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Person person = ReadPerson(dr) as Person;
                            using (SqlConnection con1 = new SqlConnection(cs))
                            {
                                con1.Open();
                                using (SqlCommand cmd1 = con1.CreateCommand())
                                {
                                    cmd1.CommandText = GetReservations;
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.AddWithValue(ReservationIdParam, person.IDPerson);
                                    using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                    {
                                        while (dr1.Read())
                                        {
                                            person.Reservation.Add(SqlRepositoryReservation.ReadReservation(dr1) as Reservation);
                                        }
                                    }
                                }
                            }
                            return (T)(person as object);
                        }
                    }
                }
            }
            throw new Exception("Person does not exist");
        }

        public static object ReadPerson(SqlDataReader dr) => new Person
        {
            IDPerson = (int)dr[nameof(Person.IDPerson)],
            FirstName = dr[nameof(Person.FirstName)].ToString(),
            LastName = dr[nameof(Person.LastName)].ToString(),
            Age = (int)dr[nameof(Person.Age)],
            Email = dr[nameof(Person.Email)].ToString(),
            Picture = ImageUtils.ByteArrayFromSqlDataReader(dr, 5)
        };

        public void Update<T>(T item)
        {
            Person person = item as Person;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = MethodBase.GetCurrentMethod().Name + typeof(T).Name;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(FirstNameParam, person.FirstName);
                    cmd.Parameters.AddWithValue(LastNameParam, person.LastName);
                    cmd.Parameters.AddWithValue(AgeParam, person.Age);
                    cmd.Parameters.AddWithValue(EmailParam, person.Email);
                    cmd.Parameters.AddWithValue(IdPersonParam, person.IDPerson);
                    cmd.Parameters.Add(new SqlParameter(PictureParam, SqlDbType.Binary, person.Picture.Length)
                    {
                        Value = person.Picture
                    });
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
