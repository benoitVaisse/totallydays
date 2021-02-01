using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totallydays.Data;
using Totallydays.Models;
using Totallydays.ViewsModel;

namespace Totallydays.Repositories
{
    public class HostingRepository
    {
        private readonly TotallydaysContext _context;

        public HostingRepository(TotallydaysContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// trouve un hébergement par son ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Hosting Find(int Id)
        {
            return this._context.Hostings.Find(Id);
        }

        /// <summary>
        /// trouve un hébergement par son ID de facon asynchrone
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Hosting> FindAsync(int Id)
        {
            return await this._context.Hostings.FindAsync(Id);
        }

        /// <summary>
        /// retourne tous les hébergements
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Hosting> FindAll()
        {
            return this._context.Hostings.ToList();
        }

        /// <summary>
        /// selectionne les hébergements d'un utilisateur
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Hosting>> FindByUser(AppUser user)
        {
            var query = await this._context.Hostings
                .Include(h => h.Bookings)
                    .ThenInclude(b => b.Rating)
                .Where(h => h.User == user)
                .ToListAsync();
           
            List<Hosting> Hostings = new List<Hosting>();
            var query2 = (from h in this._context.Hostings
                          join b in this._context.Bookings on h.Hosting_id equals b.HostingHosting_id into Bookings
                          from bo in Bookings.DefaultIfEmpty()
                          join c in this._context.Comments on bo.Booking_id equals c.BookingBooking_id into Comments
                          from co in Comments.DefaultIfEmpty()
                          where h.UserId == user.Id
                          select h).AsEnumerable().GroupBy(h => h.Hosting_id);

            
            foreach (var h in query2)
            {
                Hostings.Add(h.FirstOrDefault());
            }


            //List<Hosting> Hostings = new List<Hosting>();
            //var query2 = (from h in this._context.Hostings
            //              join b in this._context.Bookings on h.Hosting_id equals b.HostingHosting_id into Bookings
            //              from bo in Bookings.DefaultIfEmpty()
            //              join c in this._context.Comments on bo.Booking_id equals c.BookingBooking_id into Comments
            //              from co in Comments.DefaultIfEmpty()
            //              where h.UserId == user.Id
            //              group h by h.Hosting_id into hos
            //              select hos);

            //foreach (var h in query2)
            //{
            //    foreach (var ff in h)
            //    {

            //    }
            //}




            return query;
            //return Hostings;
        }

        /// <summary>
        /// search hosting by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public Hosting FindByTitle(string title)
        {
            return this._context.Hostings.Where(h => h.Title == title).FirstOrDefault();
        }

        /// <summary>
        /// get hosting by slug
        /// </summary>
        /// <param name="Slug"></param>
        /// <returns></returns>
        public Hosting FindBySlug(string Slug)
        {
            return this._context.Hostings.Where(h => h.Slug == Slug).FirstOrDefault();
        }

        /// <summary>
        /// créer un hebergement
        /// </summary>
        /// <param name="hosting"></param>
        /// <returns></returns>
        public Hosting Create(Hosting hosting)
        {
            this._context.Hostings.Add(hosting);
            this._context.SaveChanges();
            return hosting;
        }

        /// <summary>
        /// met à jour un hebergemment
        /// </summary>
        /// <param name="hosting"></param>
        /// <returns></returns>
        public Hosting Update(Hosting hosting)
        {
            this._context.Update(hosting);
            this._context.SaveChanges();
            return hosting;
        }


        /// <summary>
        /// rajoute une ligne dans la table hosting_equipment
        /// </summary>
        /// <param name="HE"></param>
        /// <returns></returns>
        public Hosting_Equipment CreateHostingEquipment(Hosting_Equipment HE)
        {
            this._context.Hosting_Equipment.Add(HE);
            this._context.SaveChanges();
            return HE;
        }

        /// <summary>
        /// supprime de la table les hosting_equipment by hosting
        /// </summary>
        /// <param name="Hosting"></param>
        /// <returns></returns>
        public Hosting DeleteHostingEquipmentByHosting(Hosting Hosting)
        {
            foreach (Hosting_Equipment HE in Hosting.Hosting_Equipment)
            {
                this._context.Hosting_Equipment.Remove(HE);
            }
            this._context.SaveChanges();
            return Hosting;
        }

        /// <summary>
        /// set publish true or false to hosting
        /// </summary>
        /// <param name="h"></param>
        /// <param name="publish"></param>
        /// <returns></returns>
        public Hosting setPublish(Hosting h, bool publish)
        {
            h.Published = publish;
            this._context.Hostings.Update(h);
            this._context.SaveChanges();
            return h;
        }


        /// <summary>
        /// return liste of hostings
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Hosting>> SearchHosting(FormSearchHostingViewModel model)
        {
            return await this._context.Hostings.FromSqlRaw<Hosting>("EXECUTE spSearchHosting {0}, {1}, {2}, {3}",
                model.City, 
                model.Start_Date, 
                model.End_Date, 
                model.Number_personn)
                .ToListAsync();
        }


        /// <summary>
        /// calculate average of one hosting
        /// </summary>
        /// <param name="hosting_id"></param>
        /// <returns></returns>
        public Double GetAverage(int hosting_id)
        {
            var query = (from h in this._context.Hostings
                        join b in this._context.Bookings on h.Hosting_id equals b.HostingHosting_id
                        join c in this._context.Comments on b.Booking_id equals c.BookingBooking_id
                        where h.Hosting_id == hosting_id
                        select c.Rating).Average();

            return Math.Round(query, 2);
        }
    }
}


public class StudentClass
{
    public enum GradeLevel { FirstYear = 1, SecondYear, ThirdYear, FourthYear };
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ID { get; set; }
        public GradeLevel Year;
        public List<int> ExamScores;
    }

    public static List<Student> students = new List<Student>
    {
        new Student {FirstName = "Terry", LastName = "Adams", ID = 120,
            Year = GradeLevel.SecondYear,
            ExamScores = new List<int>{ 99, 82, 81, 79}},
        new Student {FirstName = "Fadi", LastName = "Fakhouri", ID = 116,
            Year = GradeLevel.ThirdYear,
            ExamScores = new List<int>{ 99, 86, 90, 94}},
        new Student {FirstName = "Hanying", LastName = "Feng", ID = 117,
            Year = GradeLevel.FirstYear,
            ExamScores = new List<int>{ 93, 92, 80, 87}},
        new Student {FirstName = "Cesar", LastName = "Garcia", ID = 114,
            Year = GradeLevel.FourthYear,
            ExamScores = new List<int>{ 97, 89, 85, 82}},
        new Student {FirstName = "Debra", LastName = "Garcia", ID = 115,
            Year = GradeLevel.ThirdYear,
            ExamScores = new List<int>{ 35, 72, 91, 70}},
        new Student {FirstName = "Hugo", LastName = "Garcia", ID = 118,
            Year = GradeLevel.SecondYear,
            ExamScores = new List<int>{ 92, 90, 83, 78}},
        new Student {FirstName = "Sven", LastName = "Mortensen", ID = 113,
            Year = GradeLevel.FirstYear,
            ExamScores = new List<int>{ 88, 94, 65, 91}},
        new Student {FirstName = "Claire", LastName = "O'Donnell", ID = 112,
            Year = GradeLevel.FourthYear,
            ExamScores = new List<int>{ 75, 84, 91, 39}},
        new Student {FirstName = "Svetlana", LastName = "Omelchenko", ID = 111,
            Year = GradeLevel.SecondYear,
            ExamScores = new List<int>{ 97, 92, 81, 60}},
        new Student {FirstName = "Lance", LastName = "Tucker", ID = 119,
            Year = GradeLevel.ThirdYear,
            ExamScores = new List<int>{ 68, 79, 88, 92}},
        new Student {FirstName = "Michael", LastName = "Tucker", ID = 122,
            Year = GradeLevel.FirstYear,
            ExamScores = new List<int>{ 94, 92, 91, 91}},
        new Student {FirstName = "Eugene", LastName = "Zabokritski", ID = 121,
            Year = GradeLevel.FourthYear,
            ExamScores = new List<int>{ 96, 85, 91, 60}}
    };

    public void GroupBySingleProperty()
    {
        
    }
}