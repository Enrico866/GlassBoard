using GlassBoard.Response.Get;
using SharedLibrary.Models;
using System.Linq;

namespace GlassBoard.Mappers
{
    public static class ResourceMapper
    {
        public static ResourceItem ToDomain(this ResourceApiDto dto)
        {
            if (dto == null) return new ResourceItem();

            return new ResourceItem
            {
                Id = dto.Id,
                Name = dto.Name,
                Path = dto.Path,
                Description = dto.Description,
                IsMonitored = dto.IsMonitored, // Mappato internamente su isObserved nel DTO
                SeverityType = dto.SeverityType,
                ProbeId = dto.ProbeId,
                OrganizationId = dto.OrganizationId,
                
                // Nuovi campi hardware/OS portati nel dominio
                SerialNumber = dto.SerialNumber,
                PartNumber = dto.PartNumber,
                OsVersion = dto.Os,
                
                Tags = dto.Tags ?? new(),
                ResourceTypes = dto.ResourceTypes ?? new(),
                
                // Mapping degli attributi
                ResourceAttributes = dto.Attributes?.Select(a => new ResourceAttribute 
                { 
                    Key = a.Key, 
                    Value = a.Value 
                }).ToList() ?? new(),
                
                // Mapping degli accessi (endpoint)
                ResourceAccesses = dto.Accesses?.Select(a => new ResourceAccess 
                { 
                    Address = a.Address, 
                    AccessScopes = a.AccessScopes ?? new(),
                    Instruments = a.Instruments?.Select(i => new ResourceInstrument 
                    {
                        InstrumentType = i.InstrumentType,
                        SnmpVersion = i.SnmpVersion,
                        TimeoutSeconds = i.TimeoutSeconds,
                        SecurityId = i.SecurityId
                    }).ToList() ?? new()
                }).ToList() ?? new(),
                
                // Unione intelligente dei profili di collezione (Manuali + Auto-assegnati)
                CollectionProfileIds = (dto.CollectionProfileIds ?? new List<string>())
                    .Union(dto.AutoAssignedCollectionProfileIds ?? new List<string>())
                    .ToList(),
                
                CheckProfileIds = dto.CheckProfileIds ?? new(),
                AlertProfileIds = dto.AlertProfileIds ?? new(), // Aggiunto alert profile
                SecurityIds = dto.SecurityIds ?? new(),
                
                // Timestamp
                LastMetricsTimestamp = dto.LastMetricsTimestamp,
                AvailableMetricNames = dto.AvailableMetricNames ?? new(),
                
                // Informazioni sul modificatore
                ModifiedOn = dto.ModifiedOn,
                LastModifiedBy = dto.Modifier != null ? $"{dto.Modifier.FirstName} {dto.Modifier.LastName}" : "System"
            };
        }
    }
}