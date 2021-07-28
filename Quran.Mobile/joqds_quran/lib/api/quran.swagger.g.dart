// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'quran.swagger.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

AyatChunkDto _$AyatChunkDtoFromJson(Map json) {
  return $checkedNew('AyatChunkDto', json, () {
    final val = AyatChunkDto(
      ayat: $checkedConvert(
              json,
              'ayat',
              (v) => (v as List<dynamic>?)
                  ?.map((e) =>
                      AyahDto.fromJson(Map<String, dynamic>.from(e as Map)))
                  .toList()) ??
          [],
      fromPage: $checkedConvert(json, 'fromPage', (v) => v as int?),
      toPage: $checkedConvert(json, 'toPage', (v) => v as int?),
    );
    return val;
  });
}

Map<String, dynamic> _$AyatChunkDtoToJson(AyatChunkDto instance) =>
    <String, dynamic>{
      'ayat': instance.ayat?.map((e) => e.toJson()).toList(),
      'fromPage': instance.fromPage,
      'toPage': instance.toPage,
    };

AyahDto _$AyahDtoFromJson(Map json) {
  return $checkedNew('AyahDto', json, () {
    final val = AyahDto(
      id: $checkedConvert(json, 'id', (v) => v as int?),
      text: $checkedConvert(json, 'text', (v) => v as String?),
      surahId: $checkedConvert(json, 'surahId', (v) => v as int?),
      surahName: $checkedConvert(json, 'surahName', (v) => v as String?),
      rubId: $checkedConvert(json, 'rubId', (v) => v as int?),
      ayahInSurah: $checkedConvert(json, 'ayahInSurah', (v) => v as int?),
      rubJoz: $checkedConvert(json, 'rubJoz', (v) => v as int?),
      rubRubInJoz: $checkedConvert(json, 'rubRubInJoz', (v) => v as int?),
      pageId: $checkedConvert(json, 'pageId', (v) => v as int?),
      sajdahType: $checkedConvert(
          json, 'sajdahType', (v) => sajdahTypeFromJson(v as String?)),
      sajdahReason: $checkedConvert(json, 'sajdahReason', (v) => v as String?),
    );
    return val;
  });
}

Map<String, dynamic> _$AyahDtoToJson(AyahDto instance) => <String, dynamic>{
      'id': instance.id,
      'text': instance.text,
      'surahId': instance.surahId,
      'surahName': instance.surahName,
      'rubId': instance.rubId,
      'ayahInSurah': instance.ayahInSurah,
      'rubJoz': instance.rubJoz,
      'rubRubInJoz': instance.rubRubInJoz,
      'pageId': instance.pageId,
      'sajdahType': sajdahTypeToJson(instance.sajdahType),
      'sajdahReason': instance.sajdahReason,
    };

SurahChunkDto _$SurahChunkDtoFromJson(Map json) {
  return $checkedNew('SurahChunkDto', json, () {
    final val = SurahChunkDto(
      ayat: $checkedConvert(
              json,
              'ayat',
              (v) => (v as List<dynamic>?)
                  ?.map((e) =>
                      AyahDto.fromJson(Map<String, dynamic>.from(e as Map)))
                  .toList()) ??
          [],
      surahFromPage: $checkedConvert(json, 'surahFromPage', (v) => v as int?),
      surahToPage: $checkedConvert(json, 'surahToPage', (v) => v as int?),
      isEndChunk: $checkedConvert(json, 'isEndChunk', (v) => v as bool?),
      isStartChunk: $checkedConvert(json, 'isStartChunk', (v) => v as bool?),
      isAllChunk: $checkedConvert(json, 'isAllChunk', (v) => v as bool?),
      currentChunkPages: $checkedConvert(json, 'currentChunkPages',
              (v) => (v as List<dynamic>?)?.map((e) => e as int).toList()) ??
          [],
    );
    return val;
  });
}

Map<String, dynamic> _$SurahChunkDtoToJson(SurahChunkDto instance) =>
    <String, dynamic>{
      'ayat': instance.ayat?.map((e) => e.toJson()).toList(),
      'surahFromPage': instance.surahFromPage,
      'surahToPage': instance.surahToPage,
      'isEndChunk': instance.isEndChunk,
      'isStartChunk': instance.isStartChunk,
      'isAllChunk': instance.isAllChunk,
      'currentChunkPages': instance.currentChunkPages,
    };

SurahDto _$SurahDtoFromJson(Map json) {
  return $checkedNew('SurahDto', json, () {
    final val = SurahDto(
      id: $checkedConvert(json, 'id', (v) => v as int?),
      name: $checkedConvert(json, 'name', (v) => v as String?),
      page: $checkedConvert(json, 'page', (v) => v as int?),
      placeOfRevelationType: $checkedConvert(json, 'placeOfRevelationType',
          (v) => placeOfRevelationTypeFromJson(v as String?)),
      revelationSequenceNo:
          $checkedConvert(json, 'revelationSequenceNo', (v) => v as int?),
      ayatCount: $checkedConvert(json, 'ayatCount', (v) => v as int?),
    );
    return val;
  });
}

Map<String, dynamic> _$SurahDtoToJson(SurahDto instance) => <String, dynamic>{
      'id': instance.id,
      'name': instance.name,
      'page': instance.page,
      'placeOfRevelationType':
          placeOfRevelationTypeToJson(instance.placeOfRevelationType),
      'revelationSequenceNo': instance.revelationSequenceNo,
      'ayatCount': instance.ayatCount,
    };

WeatherForecast _$WeatherForecastFromJson(Map json) {
  return $checkedNew('WeatherForecast', json, () {
    final val = WeatherForecast(
      date: $checkedConvert(
          json, 'date', (v) => v == null ? null : DateTime.parse(v as String)),
      temperatureC: $checkedConvert(json, 'temperatureC', (v) => v as int?),
      temperatureF: $checkedConvert(json, 'temperatureF', (v) => v as int?),
      summary: $checkedConvert(json, 'summary', (v) => v as String?),
    );
    return val;
  });
}

Map<String, dynamic> _$WeatherForecastToJson(WeatherForecast instance) =>
    <String, dynamic>{
      'date': instance.date?.toIso8601String(),
      'temperatureC': instance.temperatureC,
      'temperatureF': instance.temperatureF,
      'summary': instance.summary,
    };
