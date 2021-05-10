using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Database.DAOModels;
using DataModel;

namespace Database
{
    public class DataProxy
    {
        private readonly IDataSource _data;
        private IMapper _mapper;

        public DataProxy()
        {
            _data = new MockedDatabaseDataSource();
            ConfigureMapper();
        }

        public DataProxy(IDataSource dataSource)
        {
            _data = dataSource;
            ConfigureMapper();
        }

        private void ConfigureMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MixtureDAO, Mixture>()
                    .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.Components));
                cfg.CreateMap<ComponentDAO, Component>()
                    .ForPath(dest => dest.Item.Name, opt => opt.MapFrom(src => src.Item))
                    .ForMember(dest => dest.ComponentType, opt => opt.MapFrom(src => Enum.Parse(typeof(ComponentType), src.ComponentType)));
                cfg.CreateMap<Mixture, MixtureDAO>()
                    .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.Components));
                cfg.CreateMap<Component, ComponentDAO>()
                    .ForPath(dest => dest.Item, opt => opt.MapFrom(src => src.Item.Name))
                    .ForMember(dest => dest.ComponentType, opt => opt.MapFrom(src => src.ComponentType.ToString()));
            });
            _mapper = config.CreateMapper();
        }


        public List<Mixture> GetMixtures()
        {
            var mixturesDAO = _data.GetMixtures();
            return mixturesDAO.Select(mixtureDAO => _mapper.Map<Mixture>(mixtureDAO)).ToList();
        }
        public List<Component> GetComponents()
        {
            var componentsDAO = _data.GetComponents();
            return componentsDAO.Select(componentsDAO => _mapper.Map<Component>(componentsDAO)).ToList();
        }

        public void CreateMixture(Mixture mixture)
        { 
            _data.CreateMixture(_mapper.Map<MixtureDAO>(mixture));
        }
    }
}
