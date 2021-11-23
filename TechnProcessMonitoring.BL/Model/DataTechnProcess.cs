using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnProcessMonitoring.BL.Model
{
    class DataTechnProcess
    {
        #region Свойства
        /// <summary>
        /// Расход, кг/сек
        /// </summary>
        public int Consumption { get; set; }

        /// <summary>
        /// Давление, атм
        /// </summary>
        public float Pressure { get; set; }

        /// <summary>
        /// Концентрация, мг/м3
        /// </summary>
        public float Concentration { get; set; }
        #endregion
        public DataTechnProcess(int consumption, float pressure, float concentration)
        {
            #region Проверка исключений
            if (consumption < 0)
            {
                throw new ArgumentException("Расход не может быть меньше нуля", nameof(consumption));
            }

            if (pressure < 0)
            {
                throw new ArgumentException("Давление не может быть меньше нуля", nameof(pressure));
            }
            
            if (concentration < 0)
            {
                throw new ArgumentException("Концентрация не может быть меньше нуля", nameof(concentration));
            }
            #endregion
            Consumption = consumption;
            Pressure = pressure;
            Concentration = concentration;
        }
    }

}
