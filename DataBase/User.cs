using Common.Standard.Interfaces.Model;
using System.Collections.Generic;

namespace DataBase
{
    public class User : IUser
    {
        public virtual long Id { get; set; }
        /// <summary>
        /// ��� ������������
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// ���������� true ���� ������������ �������������
        /// </summary>
        public virtual bool IsAuthorized { get; set; }
    }

}
