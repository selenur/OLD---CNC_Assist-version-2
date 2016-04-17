namespace CNC_Assist
{

    /// <summary>
    /// структура описывающая координаты
    /// </summary>
    public struct PointCNC
    {
        /// <summary>
        /// Координаты
        /// </summary>
        public decimal X, Y, Z, A;
        /// <summary>
        /// Скорость и инструмент
        /// </summary>
        public bool workSpeed, InstrumentOn;

        public PointCNC(decimal _X, decimal _Y, decimal _Z, decimal _A, bool _workSpeed = false, bool _InstrumentOn = false)
        {
            X = _X;
            Y = _Y;
            Z = _Z;
            A = _A;
            workSpeed = _workSpeed;
            InstrumentOn = _InstrumentOn;
        }
    }


    





}
