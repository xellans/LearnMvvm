using System.Collections.Generic;

namespace DataBase
{
    public class User
    {
        public long Id { get; set; }
        /// <summary>
        /// ��� ������������
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ���������� true ���� ������������ �������������
        /// </summary>
        public bool IsAuthorized { get; set; }
    }

}