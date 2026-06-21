using System;
using System.Collections.Generic;

namespace AoiMeasureTool
{
    internal sealed class ProductProfileService
    {
        private readonly Dictionary<string, PreprocessSnapshot[]> _preprocessProfiles;
        private readonly Dictionary<string, ReferenceCornerSnapshot> _referenceCornerProfiles;
        private readonly Dictionary<string, List<MeasureRecord>> _measureProfiles;

        public ProductProfileService(
            Dictionary<string, PreprocessSnapshot[]> preprocessProfiles,
            Dictionary<string, ReferenceCornerSnapshot> referenceCornerProfiles,
            Dictionary<string, List<MeasureRecord>> measureProfiles)
        {
            _preprocessProfiles = preprocessProfiles;
            _referenceCornerProfiles = referenceCornerProfiles;
            _measureProfiles = measureProfiles;
        }

        public void PersistProduct(
            string productKey,
            PreprocessSnapshot[] preprocessSnapshots,
            ReferenceCornerSnapshot referenceCornerSnapshot,
            List<MeasureRecord> measureRecords)
        {
            var normalizedProductKey = NormalizeProductKey(productKey);
            _preprocessProfiles[normalizedProductKey] = ProfileDataCloner.CloneSnapshots(preprocessSnapshots);
            _referenceCornerProfiles[normalizedProductKey] = ProfileDataCloner.CloneReferenceCornerSnapshot(referenceCornerSnapshot);
            _measureProfiles[normalizedProductKey] = ProfileDataCloner.CloneMeasureRecords(measureRecords);
        }

        public ProductProfileState GetOrCreateState(string productKey)
        {
            var normalizedProductKey = NormalizeProductKey(productKey);

            PreprocessSnapshot[] preprocessSnapshots;
            if (!_preprocessProfiles.TryGetValue(normalizedProductKey, out preprocessSnapshots))
            {
                preprocessSnapshots = ProfileDataCloner.CreateDefaultPreprocessSnapshots();
                _preprocessProfiles[normalizedProductKey] = ProfileDataCloner.CloneSnapshots(preprocessSnapshots);
            }

            ReferenceCornerSnapshot referenceCornerSnapshot;
            if (!_referenceCornerProfiles.TryGetValue(normalizedProductKey, out referenceCornerSnapshot))
            {
                referenceCornerSnapshot = ProfileDataCloner.CreateDefaultReferenceCornerSnapshot();
                _referenceCornerProfiles[normalizedProductKey] = ProfileDataCloner.CloneReferenceCornerSnapshot(referenceCornerSnapshot);
            }

            List<MeasureRecord> measureRecords;
            if (!_measureProfiles.TryGetValue(normalizedProductKey, out measureRecords))
            {
                measureRecords = new List<MeasureRecord>();
                _measureProfiles[normalizedProductKey] = ProfileDataCloner.CloneMeasureRecords(measureRecords);
            }

            return new ProductProfileState(
                normalizedProductKey,
                ProfileDataCloner.CloneSnapshots(preprocessSnapshots),
                ProfileDataCloner.CloneReferenceCornerSnapshot(referenceCornerSnapshot),
                ProfileDataCloner.CloneMeasureRecords(measureRecords));
        }

        public void ReplaceAll(AppSettingsData data)
        {
            _preprocessProfiles.Clear();
            foreach (var entry in data.PreprocessProfiles)
            {
                _preprocessProfiles[entry.Key] = ProfileDataCloner.CloneSnapshots(entry.Value);
            }

            _referenceCornerProfiles.Clear();
            foreach (var entry in data.ReferenceCornerProfiles)
            {
                _referenceCornerProfiles[entry.Key] = ProfileDataCloner.CloneReferenceCornerSnapshot(entry.Value);
            }

            _measureProfiles.Clear();
            foreach (var entry in data.MeasureProfiles)
            {
                _measureProfiles[entry.Key] = ProfileDataCloner.CloneMeasureRecords(entry.Value);
            }
        }

        public void ExportTo(AppSettingsData data)
        {
            foreach (var entry in _preprocessProfiles)
            {
                data.PreprocessProfiles[entry.Key] = ProfileDataCloner.CloneSnapshots(entry.Value);
            }

            foreach (var entry in _referenceCornerProfiles)
            {
                data.ReferenceCornerProfiles[entry.Key] = ProfileDataCloner.CloneReferenceCornerSnapshot(entry.Value);
            }

            foreach (var entry in _measureProfiles)
            {
                data.MeasureProfiles[entry.Key] = ProfileDataCloner.CloneMeasureRecords(entry.Value);
            }
        }

        private static string NormalizeProductKey(string productKey)
        {
            return string.IsNullOrWhiteSpace(productKey) ? "DEFAULT" : productKey;
        }
    }

    internal sealed class ProductProfileState
    {
        public ProductProfileState(
            string productKey,
            PreprocessSnapshot[] preprocessSnapshots,
            ReferenceCornerSnapshot referenceCornerSnapshot,
            List<MeasureRecord> measureRecords)
        {
            ProductKey = productKey;
            PreprocessSnapshots = preprocessSnapshots;
            ReferenceCornerSnapshot = referenceCornerSnapshot;
            MeasureRecords = measureRecords;
        }

        public string ProductKey { get; }
        public PreprocessSnapshot[] PreprocessSnapshots { get; }
        public ReferenceCornerSnapshot ReferenceCornerSnapshot { get; }
        public List<MeasureRecord> MeasureRecords { get; }
    }
}
