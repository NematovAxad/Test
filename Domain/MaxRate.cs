using Domain.Models;
using Domain.Models.FirstSection;
using Domain.Models.Ranking.Administrations;
using Domain.Models.Ranking;
using Domain.States;
using EntityRepository;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class MaxRate
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<GRankTable, int> _gRankTable;
        private readonly IRepository<XRankTable, int> _xRankTable;
        private readonly IRepository<ARankTable, int> _aRankTable;
        private readonly IRepository<GSphere, int> _gSphere;
        private readonly IRepository<GField, int> _gField;
        private readonly IRepository<GSubField, int> _gSubField;
        private readonly IRepository<XSphere, int> _xSphere;
        private readonly IRepository<XField, int> _xField;
        private readonly IRepository<XSubField, int> _xSubField;
        private readonly IRepository<ASphere, int> _aSphere;
        private readonly IRepository<AField, int> _aField;
        private readonly IRepository<ASubField, int> _aSubField;
        private readonly IDataContext _db;

        public MaxRate(IRepository<Organizations, int> organization, 
                        IRepository<Deadline, int> deadline, 
                        IRepository<GRankTable, int> gRankTable, 
                        IRepository<XRankTable, int> xRankTable, 
                        IRepository<ARankTable, int> aRankTable, 
                        IRepository<GSphere, int> gSphere, 
                        IRepository<GField, int> gField, 
                        IRepository<GSubField, int> gSubField, 
                        IRepository<XSphere, int> xSphere, 
                        IRepository<XField, int> xField, 
                        IRepository<XSubField, int> xSubField, 
                        IRepository<ASphere, int> aSphere, 
                        IRepository<AField, int> aField, 
                        IRepository<ASubField, int> aSubField, 
                        IDataContext db)
        {
            _organization = organization;
            _deadline = deadline;
            _gRankTable = gRankTable;
            _xRankTable = xRankTable;
            _aRankTable = aRankTable;
            _gSphere = gSphere;
            _gField = gField;
            _gSubField = gSubField;
            _xSphere = xSphere;
            _xField = xField;
            _xSubField = xSubField;
            _aSphere = aSphere;
            _aField = aField;
            _aSubField = aSubField;
            _db = db;
        }

        public double FieldRate(int orgId, int fieldId, string section)
        {
            double rate = 0;

            var org = _organization.Find(o => o.Id == orgId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);

            if(org.OrgCategory == Enums.OrgCategory.Adminstrations)
            {
                var fields = _aField.GetAll();
                var field = fields.Where(f => f.Id == fieldId).FirstOrDefault();

                if(field == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                rate = field.MaxRate;
            }
            if (org.OrgCategory == Enums.OrgCategory.GovernmentOrganizations)
            {
                var fields = _gField.GetAll();
                var field = fields.Where(f => f.Id == fieldId).FirstOrDefault();

                if (field == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                rate = field.MaxRate;
            }
            if (org.OrgCategory == Enums.OrgCategory.FarmOrganizations)
            {
                var fields = _xField.GetAll();
                var field = fields.Where(f => f.Id == fieldId).FirstOrDefault();

                if (field == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                rate = field.MaxRate;
            }

            return rate;
        }
        public double SubFieldRate(int orgId, string fieldSection, string subFieldSection)
        {
            double rate = 0;

            var org = _organization.Find(o => o.Id == orgId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);

            if (org.OrgCategory == Enums.OrgCategory.Adminstrations)
            {
                var fields = _aField.GetAll().Include(mbox=>mbox.ASubFields);
                var field = fields.Where(f => f.Section == fieldSection).FirstOrDefault();

                if (field == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                var subField = field.ASubFields.Where(s => s.Section == subFieldSection).FirstOrDefault();
                
                if (subField == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                rate = subField.MaxRate;
            }
            if (org.OrgCategory == Enums.OrgCategory.GovernmentOrganizations)
            {
                var fields = _gField.GetAll();
                var field = fields.Where(f => f.Section == fieldSection).FirstOrDefault();

                if (field == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                var subField = field.GSubFields.Where(s => s.Section == subFieldSection).FirstOrDefault();

                if (subField == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                rate = subField.MaxRate;
            }
            if (org.OrgCategory == Enums.OrgCategory.FarmOrganizations)
            {
                var fields = _xField.GetAll();
                var field = fields.Where(f => f.Section == fieldSection).FirstOrDefault();

                if (field == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                var subField = field.XSubFields.Where(s => s.Section == subFieldSection).FirstOrDefault();

                if (subField == null)
                    throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);

                rate = subField.MaxRate;
            }

            return rate;
        }
    }
}
