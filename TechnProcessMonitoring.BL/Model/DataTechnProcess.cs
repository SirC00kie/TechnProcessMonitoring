using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnProcessMonitoring.BL.Model
{
    class DataTechnProcess
    {
        /// <summary>
        /// Расход, кг/сек
        /// </summary>
        public int Consumption { get; private set; }

        /// <summary>
        /// Давление, атм
        /// </summary>
        public float Pressure { get; private set; }

        /// <summary>
        /// Концентрация, мг/м3
        /// </summary>
        public float Concentration { get; private set; }
        
        public DataTechnProcess(int consumption, float pressure, float concentration)
        {
            if (consumption < 0)
            {
                throw new AggregateException(nameof(consumption));
            }

            if (pressure < 0)
            {
                throw new AggregateException(nameof(pressure));
            }
            
            if (concentration < 0)
            {
                throw new AggregateException(nameof(concentration));
            }
            
            Consumption = consumption;
            Pressure = pressure;
            Concentration = concentration;
        }
    }

}
