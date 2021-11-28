using System;

namespace TechnProcessMonitoring.BL.Model
{
    [Serializable]
    public class DataTechnProcess
    {
        #region Свойства
        /// <summary>
        /// Расход, кг/сек
        /// </summary>
        public double Consumption { get; set; }

        /// <summary>
        /// Давление, атм
        /// </summary>
        public double Pressure { get; set; }

        /// <summary>
        /// Концентрация, мг/м3
        /// </summary>
        public double Concentration { get; set; }
        #endregion
        public DataTechnProcess(double consumption, double pressure, double concentration)
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
