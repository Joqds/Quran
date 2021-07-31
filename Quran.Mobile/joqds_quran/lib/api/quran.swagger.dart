// ignore_for_file: non_constant_identifier_names, hash_and_equals

import 'package:json_annotation/json_annotation.dart';
import 'package:collection/collection.dart';

import 'package:chopper/chopper.dart';
import 'package:chopper/chopper.dart' as chopper;
import 'quran.enums.swagger.dart' as enums;
export 'quran.enums.swagger.dart';

part 'quran.swagger.chopper.dart';
part 'quran.swagger.g.dart';

// **************************************************************************
// SwaggerChopperGenerator
// **************************************************************************

@ChopperApi()
abstract class Quran extends ChopperService {
  static Quran create([ChopperClient? client]) {
    if (client != null) {
      return _$Quran(client);
    }

    final newClient = ChopperClient(
        services: [_$Quran()],
        converter: JsonSerializableConverter(),
        baseUrl: 'https://quran.api.joqds.ir');
    return _$Quran(newClient);
  }

  ///
  ///@param startPage
  ///@param endPage

  @Get(path: '/api/Quran/GetAyatByPage')
  Future<chopper.Response<AyatChunkDto>> quranGetAyatByPage(
      {@Query('startPage') int? startPage, @Query('endPage') int? endPage});

  ///
  ///@param surahId
  ///@param startPage
  ///@param endPage

  @Get(path: '/api/Quran/GetAyatBySurah')
  Future<chopper.Response<SurahChunkDto>> quranGetAyatBySurah(
      {@Query('surahId') int? surahId,
      @Query('startPage') int? startPage,
      @Query('endPage') int? endPage});

  ///
  ///@param rubId

  @Get(path: '/api/Quran/GetAyatByRub')
  Future<chopper.Response<AyatChunkDto>> quranGetAyatByRub(
      {@Query('rubId') int? rubId});

  ///
  ///@param jozId

  @Get(path: '/api/Quran/GetAyatByJoz')
  Future<chopper.Response<AyatChunkDto>> quranGetAyatByJoz(
      {@Query('jozId') int? jozId});

  ///

  @Get(path: '/api/Quran/GetSurahList')
  Future<chopper.Response<List<SurahDto>>> quranGetSurahList();

  ///

  @Get(path: '/api/WeatherForecast')
  Future<chopper.Response<List<WeatherForecast>>> weatherForecastGet();
}

final Map<Type, Object Function(Map<String, dynamic>)>
    QuranJsonDecoderMappings = {
  AyatChunkDto: AyatChunkDto.fromJsonFactory,
  AyahDto: AyahDto.fromJsonFactory,
  SurahChunkDto: SurahChunkDto.fromJsonFactory,
  SurahDto: SurahDto.fromJsonFactory,
  WeatherForecast: WeatherForecast.fromJsonFactory,
};

@JsonSerializable(explicitToJson: true)
class AyatChunkDto {
  AyatChunkDto({
    this.ayat,
    this.fromPage,
    this.toPage,
  });

  factory AyatChunkDto.fromJson(Map<String, dynamic> json) =>
      _$AyatChunkDtoFromJson(json);

  @JsonKey(name: 'ayat', defaultValue: <AyahDto>[])
  final List<AyahDto>? ayat;
  @JsonKey(name: 'fromPage')
  final int? fromPage;
  @JsonKey(name: 'toPage')
  final int? toPage;
  static const fromJsonFactory = _$AyatChunkDtoFromJson;
  static const toJsonFactory = _$AyatChunkDtoToJson;
  Map<String, dynamic> toJson() => _$AyatChunkDtoToJson(this);

  @override
  bool operator ==(dynamic other) {
    return identical(this, other) ||
        (other is AyatChunkDto &&
            (identical(other.ayat, ayat) ||
                const DeepCollectionEquality().equals(other.ayat, ayat)) &&
            (identical(other.fromPage, fromPage) ||
                const DeepCollectionEquality()
                    .equals(other.fromPage, fromPage)) &&
            (identical(other.toPage, toPage) ||
                const DeepCollectionEquality().equals(other.toPage, toPage)));
  }
}

extension $AyatChunkDtoExtension on AyatChunkDto {
  AyatChunkDto copyWith({List<AyahDto>? ayat, int? fromPage, int? toPage}) {
    return AyatChunkDto(
        ayat: ayat ?? this.ayat,
        fromPage: fromPage ?? this.fromPage,
        toPage: toPage ?? this.toPage);
  }
}

@JsonSerializable(explicitToJson: true)
class AyahDto {
  AyahDto({
    this.id,
    this.text,
    this.surahId,
    this.surahName,
    this.rubId,
    this.ayahInSurah,
    this.rubJoz,
    this.rubRubInJoz,
    this.pageId,
    this.sajdahType,
    this.sajdahReason,
  });

  factory AyahDto.fromJson(Map<String, dynamic> json) =>
      _$AyahDtoFromJson(json);

  @JsonKey(name: 'id')
  final int? id;
  @JsonKey(name: 'text')
  final String? text;
  @JsonKey(name: 'surahId')
  final int? surahId;
  @JsonKey(name: 'surahName')
  final String? surahName;
  @JsonKey(name: 'rubId')
  final int? rubId;
  @JsonKey(name: 'ayahInSurah')
  final int? ayahInSurah;
  @JsonKey(name: 'rubJoz')
  final int? rubJoz;
  @JsonKey(name: 'rubRubInJoz')
  final int? rubRubInJoz;
  @JsonKey(name: 'pageId')
  final int? pageId;
  @JsonKey(
      name: 'sajdahType',
      toJson: sajdahTypeToJson,
      fromJson: sajdahTypeFromJson)
  final enums.SajdahType? sajdahType;
  @JsonKey(name: 'sajdahReason')
  final String? sajdahReason;
  static const fromJsonFactory = _$AyahDtoFromJson;
  static const toJsonFactory = _$AyahDtoToJson;
  Map<String, dynamic> toJson() => _$AyahDtoToJson(this);

  @override
  bool operator ==(dynamic other) {
    return identical(this, other) ||
        (other is AyahDto &&
            (identical(other.id, id) ||
                const DeepCollectionEquality().equals(other.id, id)) &&
            (identical(other.text, text) ||
                const DeepCollectionEquality().equals(other.text, text)) &&
            (identical(other.surahId, surahId) ||
                const DeepCollectionEquality()
                    .equals(other.surahId, surahId)) &&
            (identical(other.surahName, surahName) ||
                const DeepCollectionEquality()
                    .equals(other.surahName, surahName)) &&
            (identical(other.rubId, rubId) ||
                const DeepCollectionEquality().equals(other.rubId, rubId)) &&
            (identical(other.ayahInSurah, ayahInSurah) ||
                const DeepCollectionEquality()
                    .equals(other.ayahInSurah, ayahInSurah)) &&
            (identical(other.rubJoz, rubJoz) ||
                const DeepCollectionEquality().equals(other.rubJoz, rubJoz)) &&
            (identical(other.rubRubInJoz, rubRubInJoz) ||
                const DeepCollectionEquality()
                    .equals(other.rubRubInJoz, rubRubInJoz)) &&
            (identical(other.pageId, pageId) ||
                const DeepCollectionEquality().equals(other.pageId, pageId)) &&
            (identical(other.sajdahType, sajdahType) ||
                const DeepCollectionEquality()
                    .equals(other.sajdahType, sajdahType)) &&
            (identical(other.sajdahReason, sajdahReason) ||
                const DeepCollectionEquality()
                    .equals(other.sajdahReason, sajdahReason)));
  }
}

extension $AyahDtoExtension on AyahDto {
  AyahDto copyWith(
      {int? id,
      String? text,
      int? surahId,
      String? surahName,
      int? rubId,
      int? ayahInSurah,
      int? rubJoz,
      int? rubRubInJoz,
      int? pageId,
      enums.SajdahType? sajdahType,
      String? sajdahReason}) {
    return AyahDto(
        id: id ?? this.id,
        text: text ?? this.text,
        surahId: surahId ?? this.surahId,
        surahName: surahName ?? this.surahName,
        rubId: rubId ?? this.rubId,
        ayahInSurah: ayahInSurah ?? this.ayahInSurah,
        rubJoz: rubJoz ?? this.rubJoz,
        rubRubInJoz: rubRubInJoz ?? this.rubRubInJoz,
        pageId: pageId ?? this.pageId,
        sajdahType: sajdahType ?? this.sajdahType,
        sajdahReason: sajdahReason ?? this.sajdahReason);
  }
}

@JsonSerializable(explicitToJson: true)
class SurahChunkDto {
  SurahChunkDto({
    this.ayat,
    this.surahFromPage,
    this.surahToPage,
    this.isEndChunk,
    this.isStartChunk,
    this.isAllChunk,
    this.currentChunkPages,
  });

  factory SurahChunkDto.fromJson(Map<String, dynamic> json) =>
      _$SurahChunkDtoFromJson(json);

  @JsonKey(name: 'ayat', defaultValue: <AyahDto>[])
  final List<AyahDto>? ayat;
  @JsonKey(name: 'surahFromPage')
  final int? surahFromPage;
  @JsonKey(name: 'surahToPage')
  final int? surahToPage;
  @JsonKey(name: 'isEndChunk')
  final bool? isEndChunk;
  @JsonKey(name: 'isStartChunk')
  final bool? isStartChunk;
  @JsonKey(name: 'isAllChunk')
  final bool? isAllChunk;
  @JsonKey(name: 'currentChunkPages', defaultValue: <int>[])
  final List<int>? currentChunkPages;
  static const fromJsonFactory = _$SurahChunkDtoFromJson;
  static const toJsonFactory = _$SurahChunkDtoToJson;
  Map<String, dynamic> toJson() => _$SurahChunkDtoToJson(this);

  @override
  bool operator ==(dynamic other) {
    return identical(this, other) ||
        (other is SurahChunkDto &&
            (identical(other.ayat, ayat) ||
                const DeepCollectionEquality().equals(other.ayat, ayat)) &&
            (identical(other.surahFromPage, surahFromPage) ||
                const DeepCollectionEquality()
                    .equals(other.surahFromPage, surahFromPage)) &&
            (identical(other.surahToPage, surahToPage) ||
                const DeepCollectionEquality()
                    .equals(other.surahToPage, surahToPage)) &&
            (identical(other.isEndChunk, isEndChunk) ||
                const DeepCollectionEquality()
                    .equals(other.isEndChunk, isEndChunk)) &&
            (identical(other.isStartChunk, isStartChunk) ||
                const DeepCollectionEquality()
                    .equals(other.isStartChunk, isStartChunk)) &&
            (identical(other.isAllChunk, isAllChunk) ||
                const DeepCollectionEquality()
                    .equals(other.isAllChunk, isAllChunk)) &&
            (identical(other.currentChunkPages, currentChunkPages) ||
                const DeepCollectionEquality()
                    .equals(other.currentChunkPages, currentChunkPages)));
  }
}

extension $SurahChunkDtoExtension on SurahChunkDto {
  SurahChunkDto copyWith(
      {List<AyahDto>? ayat,
      int? surahFromPage,
      int? surahToPage,
      bool? isEndChunk,
      bool? isStartChunk,
      bool? isAllChunk,
      List<int>? currentChunkPages}) {
    return SurahChunkDto(
        ayat: ayat ?? this.ayat,
        surahFromPage: surahFromPage ?? this.surahFromPage,
        surahToPage: surahToPage ?? this.surahToPage,
        isEndChunk: isEndChunk ?? this.isEndChunk,
        isStartChunk: isStartChunk ?? this.isStartChunk,
        isAllChunk: isAllChunk ?? this.isAllChunk,
        currentChunkPages: currentChunkPages ?? this.currentChunkPages);
  }
}

@JsonSerializable(explicitToJson: true)
class SurahDto {
  SurahDto({
    this.id,
    this.name,
    this.page,
    this.placeOfRevelationType,
    this.revelationSequenceNo,
    this.ayatCount,
  });

  factory SurahDto.fromJson(Map<String, dynamic> json) =>
      _$SurahDtoFromJson(json);

  @JsonKey(name: 'id')
  final int? id;
  @JsonKey(name: 'name')
  final String? name;
  @JsonKey(name: 'page')
  final int? page;
  @JsonKey(
      name: 'placeOfRevelationType',
      toJson: placeOfRevelationTypeToJson,
      fromJson: placeOfRevelationTypeFromJson)
  final enums.PlaceOfRevelationType? placeOfRevelationType;
  @JsonKey(name: 'revelationSequenceNo')
  final int? revelationSequenceNo;
  @JsonKey(name: 'ayatCount')
  final int? ayatCount;
  static const fromJsonFactory = _$SurahDtoFromJson;
  static const toJsonFactory = _$SurahDtoToJson;
  Map<String, dynamic> toJson() => _$SurahDtoToJson(this);

  @override
  bool operator ==(dynamic other) {
    return identical(this, other) ||
        (other is SurahDto &&
            (identical(other.id, id) ||
                const DeepCollectionEquality().equals(other.id, id)) &&
            (identical(other.name, name) ||
                const DeepCollectionEquality().equals(other.name, name)) &&
            (identical(other.page, page) ||
                const DeepCollectionEquality().equals(other.page, page)) &&
            (identical(other.placeOfRevelationType, placeOfRevelationType) ||
                const DeepCollectionEquality().equals(
                    other.placeOfRevelationType, placeOfRevelationType)) &&
            (identical(other.revelationSequenceNo, revelationSequenceNo) ||
                const DeepCollectionEquality().equals(
                    other.revelationSequenceNo, revelationSequenceNo)) &&
            (identical(other.ayatCount, ayatCount) ||
                const DeepCollectionEquality()
                    .equals(other.ayatCount, ayatCount)));
  }
}

extension $SurahDtoExtension on SurahDto {
  SurahDto copyWith(
      {int? id,
      String? name,
      int? page,
      enums.PlaceOfRevelationType? placeOfRevelationType,
      int? revelationSequenceNo,
      int? ayatCount}) {
    return SurahDto(
        id: id ?? this.id,
        name: name ?? this.name,
        page: page ?? this.page,
        placeOfRevelationType:
            placeOfRevelationType ?? this.placeOfRevelationType,
        revelationSequenceNo: revelationSequenceNo ?? this.revelationSequenceNo,
        ayatCount: ayatCount ?? this.ayatCount);
  }
}

@JsonSerializable(explicitToJson: true)
class WeatherForecast {
  WeatherForecast({
    this.date,
    this.temperatureC,
    this.temperatureF,
    this.summary,
  });

  factory WeatherForecast.fromJson(Map<String, dynamic> json) =>
      _$WeatherForecastFromJson(json);

  @JsonKey(name: 'date')
  final DateTime? date;
  @JsonKey(name: 'temperatureC')
  final int? temperatureC;
  @JsonKey(name: 'temperatureF')
  final int? temperatureF;
  @JsonKey(name: 'summary')
  final String? summary;
  static const fromJsonFactory = _$WeatherForecastFromJson;
  static const toJsonFactory = _$WeatherForecastToJson;
  Map<String, dynamic> toJson() => _$WeatherForecastToJson(this);

  @override
  bool operator ==(dynamic other) {
    return identical(this, other) ||
        (other is WeatherForecast &&
            (identical(other.date, date) ||
                const DeepCollectionEquality().equals(other.date, date)) &&
            (identical(other.temperatureC, temperatureC) ||
                const DeepCollectionEquality()
                    .equals(other.temperatureC, temperatureC)) &&
            (identical(other.temperatureF, temperatureF) ||
                const DeepCollectionEquality()
                    .equals(other.temperatureF, temperatureF)) &&
            (identical(other.summary, summary) ||
                const DeepCollectionEquality().equals(other.summary, summary)));
  }
}

extension $WeatherForecastExtension on WeatherForecast {
  WeatherForecast copyWith(
      {DateTime? date, int? temperatureC, int? temperatureF, String? summary}) {
    return WeatherForecast(
        date: date ?? this.date,
        temperatureC: temperatureC ?? this.temperatureC,
        temperatureF: temperatureF ?? this.temperatureF,
        summary: summary ?? this.summary);
  }
}

String? sajdahTypeToJson(enums.SajdahType? sajdahType) {
  return enums.$SajdahTypeMap[sajdahType];
}

enums.SajdahType sajdahTypeFromJson(String? sajdahType) {
  if (sajdahType == null) {
    return enums.SajdahType.swaggerGeneratedUnknown;
  }

  return enums.$SajdahTypeMap.entries
      .firstWhere(
          (element) => element.value.toLowerCase() == sajdahType.toLowerCase(),
          orElse: () =>
              const MapEntry(enums.SajdahType.swaggerGeneratedUnknown, ''))
      .key;
}

List<String> sajdahTypeListToJson(List<enums.SajdahType>? sajdahType) {
  if (sajdahType == null) {
    return [];
  }

  return sajdahType.map((e) => enums.$SajdahTypeMap[e]!).toList();
}

List<enums.SajdahType> sajdahTypeListFromJson(List? sajdahType) {
  if (sajdahType == null) {
    return [];
  }

  return sajdahType.map((e) => sajdahTypeFromJson(e.toString())).toList();
}

String? placeOfRevelationTypeToJson(
    enums.PlaceOfRevelationType? placeOfRevelationType) {
  return enums.$PlaceOfRevelationTypeMap[placeOfRevelationType];
}

enums.PlaceOfRevelationType placeOfRevelationTypeFromJson(
    String? placeOfRevelationType) {
  if (placeOfRevelationType == null) {
    return enums.PlaceOfRevelationType.swaggerGeneratedUnknown;
  }

  return enums.$PlaceOfRevelationTypeMap.entries
      .firstWhere(
          (element) =>
              element.value.toLowerCase() ==
              placeOfRevelationType.toLowerCase(),
          orElse: () => const MapEntry(
              enums.PlaceOfRevelationType.swaggerGeneratedUnknown, ''))
      .key;
}

List<String> placeOfRevelationTypeListToJson(
    List<enums.PlaceOfRevelationType>? placeOfRevelationType) {
  if (placeOfRevelationType == null) {
    return [];
  }

  return placeOfRevelationType
      .map((e) => enums.$PlaceOfRevelationTypeMap[e]!)
      .toList();
}

List<enums.PlaceOfRevelationType> placeOfRevelationTypeListFromJson(
    List? placeOfRevelationType) {
  if (placeOfRevelationType == null) {
    return [];
  }

  return placeOfRevelationType
      .map((e) => placeOfRevelationTypeFromJson(e.toString()))
      .toList();
}

typedef JsonFactory<T> = T Function(Map<String, dynamic> json);

class CustomJsonDecoder {
  CustomJsonDecoder(this.factories);

  final Map<Type, JsonFactory> factories;

  dynamic decode<T>(dynamic entity) {
    if (entity is Iterable) {
      return _decodeList<T>(entity);
    }

    if (entity is T) {
      return entity;
    }

    if (entity is Map<String, dynamic>) {
      return _decodeMap<T>(entity);
    }

    return entity;
  }

  T _decodeMap<T>(Map<String, dynamic> values) {
    final jsonFactory = factories[T];
    if (jsonFactory == null || jsonFactory is! JsonFactory<T>) {
      return throw "Could not find factory for type $T. Is '$T: $T.fromJsonFactory' included in the CustomJsonDecoder instance creation in bootstrapper.dart?";
    }

    return jsonFactory(values);
  }

  List<T> _decodeList<T>(Iterable values) =>
      values.where((v) => v != null).map<T>((v) => decode<T>(v) as T).toList();
}

class JsonSerializableConverter extends chopper.JsonConverter {
  @override
  chopper.Response<ResultType> convertResponse<ResultType, Item>(
      chopper.Response response) {
    if (response.bodyString.isEmpty) {
      // In rare cases, when let's say 204 (no content) is returned -
      // we cannot decode the missing json with the result type specified
      return chopper.Response(response.base, null, error: response.error);
    }

    final jsonRes = super.convertResponse(response);
    return jsonRes.copyWith<ResultType>(
        body: jsonDecoder.decode<Item>(jsonRes.body) as ResultType);
  }
}

final jsonDecoder = CustomJsonDecoder(QuranJsonDecoderMappings);

// ignore: unused_element
String? _dateToJson(DateTime? date) {
  if (date == null) {
    return null;
  }

  final year = date.year.toString();
  final month = date.month < 10 ? '0${date.month}' : date.month.toString();
  final day = date.day < 10 ? '0${date.day}' : date.day.toString();

  return '$year-$month-$day';
}
