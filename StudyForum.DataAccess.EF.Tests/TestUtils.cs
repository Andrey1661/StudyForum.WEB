using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using StudyForum.DataAccess.Enviroment;

namespace StudyForum.DataAccess.EF.Tests
{
    public static class TestUtils
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutomapperDataAccessModule>();
            });

            return config.CreateMapper();
        }
    }
}
