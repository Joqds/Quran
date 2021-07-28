// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'quran.swagger.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

AyatChunkDto _$AyatChunkDtoFromJson(Map<String, dynamic> json) {
  return AyatChunkDto(
    ayat: (json['ayat'] as List<dynamic>?)
            ?.map((e) => AyahDto.fromJson(e as Map<String, dynamic>))
            .toList() ??
        [],
    fromPage: json['fromPage'] as int?,
    toPage: json['toPage'] as int?,
  );
}

Map<String, dynamic> _$AyatChunkDtoToJson(AyatChunkDto instance) =>
    <String, dynamic>{
      'ayat': instance.ayat?.map((e) => e.toJson()).toList(),
      'fromPage': instance.fromPage,
      'toPage': instance.toPage,
    };

AyahDto _$AyahDtoFromJson(Map<String, dynamic> json) {
  return AyahDto(
    id: json['id'] as int?,
    text: json['text'] as String?,
    surahId: json['surahId'] as int?,
    surahName: json['surahName'] as String?,
    rubId: json['rubId'] as int?,
    ayahInSurah: json['ayahInSurah'] as int?,
    rubJoz: json['rubJoz'] as int?,
    rubRubInJoz: json['rubRubInJoz'] as int?,
    pageId: json['pageId'] as int?,
    sajdahType: sajdahTypeFromJson(json['sajdahType'] as String?),
    sajdahReason: json['sajdahReason'] as String?,
  );
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

SurahChunkDto _$SurahChunkDtoFromJson(Map<String, dynamic> json) {
  return SurahChunkDto(
    ayat: (json['ayat'] as List<dynamic>?)
            ?.map((e) => AyahDto.fromJson(e as Map<String, dynamic>))
            .toList() ??
        [],
    surahFromPage: json['surahFromPage'] as int?,
    surahToPage: json['surahToPage'] as int?,
    isEndChunk: json['isEndChunk'] as bool?,
    isStartChunk: json['isStartChunk'] as bool?,
    isAllChunk: json['isAllChunk'] as bool?,
    currentChunkPages: (json['currentChunkPages'] as List<dynamic>?)
            ?.map((e) => e as int)
            .toList() ??
        [],
  );
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

SurahDto _$SurahDtoFromJson(Map<String, dynamic> json) {
  return SurahDto(
    id: json['id'] as int?,
    name: json['name'] as String?,
    page: json['page'] as int?,
    placeOfRevelationType:
        placeOfRevelationTypeFromJson(json['placeOfRevelationType'] as String?),
    revelationSequenceNo: json['revelationSequenceNo'] as int?,
    ayatCount: json['ayatCount'] as int?,
  );
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

WeatherForecast _$WeatherForecastFromJson(Map<String, dynamic> json) {
  return WeatherForecast(
    date: json['date'] == null ? null : DateTime.parse(json['date'] as String),
    temperatureC: json['temperatureC'] as int?,
    temperatureF: json['temperatureF'] as int?,
    summary: json['summary'] as String?,
  );
}

Map<String, dynamic> _$WeatherForecastToJson(WeatherForecast instance) =>
    <String, dynamic>{
      'date': instance.date?.toIso8601String(),
      'temperatureC': instance.temperatureC,
      'temperatureF': instance.temperatureF,
      'summary': instance.summary,
    };
