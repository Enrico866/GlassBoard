using GlassBoard.Response.Get;
using SharedLibrary.Models;

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
                IsMonitored = dto.IsMonitored,
                SeverityType = dto.SeverityType,
                ProbeId = dto.ProbeId,
                OrganizationId = dto.OrganizationId,
                Tags = dto.Tags ?? new(),
                ResourceTypes = dto.ResourceTypes ?? new(),
                ResourceAttributes = dto.Attributes?.Select(a => new ResourceAttribute { Key = a.Key, Value = a.Value }).ToList() ?? new(),
                ResourceAccesses = dto.Accesses?.Select(a => new ResourceAccess { 
                    Address = a.Address, 
                    Instruments = a.Instruments, 
                    AccessScopes = a.AccessScopes 
                }).ToList() ?? new(),
                CollectionProfileIds = (dto.CollectionProfileIds ?? new List<string>())
                                        .Concat(dto.AutoAssignedCollectionProfileIds ?? new List<string>()).ToList(),
                CheckProfileIds = dto.CheckProfileIds ?? new(),
                SecurityIds = dto.SecurityIds ?? new(),
                LastMetricsTimestamp = dto.LastMetricsTimestamp,
                AvailableMetricNames = dto.AvailableMetricNames ?? new()
            };
        }
    }
}