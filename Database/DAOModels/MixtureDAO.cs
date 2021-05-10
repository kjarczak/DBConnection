using System.Collections.Generic;

namespace Database.DAOModels
{
    public class MixtureDAO
    {
        public string Name { get; set; }
        public List<ComponentDAO> Components { get; set; }
    }
}
