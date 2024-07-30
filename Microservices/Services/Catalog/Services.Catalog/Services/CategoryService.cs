﻿using AutoMapper;
using MongoDB.Driver;
using Services.Catalog.Dtos;
using Services.Catalog.Models;
using Services.Catalog.Settings;
using Shared.Dtos;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Services.Catalog.Services
{
    public class CategoryService
    {
        private readonly IMongoCollection<Category> _categoriesCollection;

        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConntectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categoriesCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoriesCollection.Find(category => true).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<Response<CategoryDto>> CreateAsync(Category category)
        {
            await _categoriesCollection.InsertOneAsync(category);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }

        public async Task<Response<CategoryDto>> GetByAsync(string id)
        {
            var category = await _categoriesCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();

            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found", 404);
            }
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }

    }
}