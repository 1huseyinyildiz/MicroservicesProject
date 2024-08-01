using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using Services.Catalog.Dtos;
using Services.Catalog.Models;
using Services.Catalog.Settings;
using Shared.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Catalog.Services
{
    public class CourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings, IMongoCollection<Course> categoryCollection)
        {
            var client = new MongoClient(databaseSettings.ConntectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection=database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

            _mapper = mapper;
              
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course =>course.Id== course.CategoryId).FirstAsync();

            if (courses.Any())
            {
                foreach (var course in courses)
                {
                    course.Id = course.Id;
                    course.Name = course.Name;
                    course.Description = course.Description;
                }
            }

            return null;
        }
    }
}
