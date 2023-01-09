using JohaRepository.Exception;
using SB.Common.Logics.SynonymProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.States
{
    public static class ErrorStates
    {
        public static RepoException AccessDenied()
        {
            return new RepoException(102, 403, "AccessDenied");
        }
        public static RepoException NotEntered(string FieldName)
        {
            return new RepoException(FieldName + " не введено!");
        }
        public static RepoException NotChoosen(string FieldName)
        {
            return new RepoException(FieldName + " не выбрано!");
        }
        public static RepoException NotFound(string FieldName)
        {
            return new RepoException(FieldName + " не найдено!");
        }
        public static RepoException NotAllowed(string FieldName)
        {
            return new RepoException(400, FieldName + " не допускается!");
        }
        public static RepoException WrongData(string FieldName)
        {
            return new RepoException(FieldName);
        }
        public static RepoException NotResponding()
        {
            return new RepoException(504, "OneId not Responding");
        }
        public static RepoException CyberSecurityServiceNotWorking()
        {
            return new RepoException(504, "CyberSecurity not Responding");
        }

        public static RepoException Error(Enum errorEnum)
        {
            string message = EnumSynonymProvider.Get(errorEnum);
            return new RepoException(message);
        }
    }
}
