using GAE.AIQ.API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Handlers.FunctionalCharacteristic
{
    public class FunctionalCharacteristicSingleton
    {
        private static readonly FunctionalCharacteristicSingleton _instance = new FunctionalCharacteristicSingleton();
        private static List<Data.Entities.FunctionalCharacteristic> _listFunctionalCharacteristics = null;


        public FunctionalCharacteristicSingleton()
        {

        }
        public static FunctionalCharacteristicSingleton Instance
        {
            get
            {
                return _instance;
            }
        }
        public async Task<List<Data.Entities.FunctionalCharacteristic>> Characteristics(ApplicationDbContext dbContext)
        {

            if (_listFunctionalCharacteristics == null)
            {
                _listFunctionalCharacteristics = await dbContext.FunctionalCharacteristics.ToListAsync();
            }
            return _listFunctionalCharacteristics;
        }
    }
}
