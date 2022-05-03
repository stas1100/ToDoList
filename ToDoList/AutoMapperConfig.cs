using AutoMapper;
using ToDoList.ViewModels;
using ToDoList.Models;

namespace ToDoList
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Configure()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => {
                cfg.CreateMap<ToDoTaskCreateViewModel, ToDoTaskModel>();
                cfg.CreateMap<ToDoTaskViewModel, ToDoTaskModel>().ReverseMap();
                cfg.CreateMap<ToDoTaskEditViewModel, ToDoTaskModel>().ReverseMap();
                cfg.CreateMap<CategoryDeleteViewModel, CategoryModel>();
                cfg.CreateMap<CategoryCreateViewModel, CategoryModel>();
                cfg.CreateMap<CategoryViewModel, CategoryModel>().ReverseMap();
            });
            mapperConfiguration.CreateMapper();
            return mapperConfiguration;
        }
    }
}
