using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Database.DAOModels;
using DataModel;

namespace Database
{
    public class DataProxy : IDataProxy
    {
        private readonly IDataSource _dataSource;
        private IMapper _mapper;

        public DataProxy(IDataSource dataSourceSource)
        {
            _dataSource = dataSourceSource;
            ConfigureMapper();
        }
        
        public List<Mixture> GetMixtures()
        {
            var mixturesDAO = _dataSource.GetMixtures();
            return mixturesDAO.Select(mixtureDAO => _mapper.Map<Mixture>(mixtureDAO)).ToList();
        }
        public List<Component> GetComponents()
        {
            var componentsDAO = _dataSource.GetComponents();
            return componentsDAO.Select(components => _mapper.Map<Component>(components)).ToList();
        }
        public void CreateMixture(Mixture mixture)
        { 
            _dataSource.CreateMixture(_mapper.Map<MixtureDAO>(mixture));
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
    }
}
