using System;
using System.Collections.Generic;
using System.Text;

namespace CNC_Assist
{

    /// <summary>
    /// Через данный класс будем работать с котроллером, по поводу отправки данных в контроллер
    /// </summary>
    public static class TaskManager
    {
        private static bool _isAvailable;


        static TaskManager()
        {
            isAvailable = false;
        }


        /// <summary>
        /// Описывает возможность использования менеджера заданий, менеджер доступен для использования, только при условии установки связи с контроллером
        /// </summary>
        /// <value>булево - Доступен ли для использования</value>
        public static bool isAvailable
        {
            get { return _isAvailable; }
            set { _isAvailable = value; }
        }
    }
}
