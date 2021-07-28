//Generated code

part of 'quran.swagger.dart';

// **************************************************************************
// ChopperGenerator
// **************************************************************************

// ignore_for_file: always_put_control_body_on_new_line, always_specify_types, prefer_const_declarations
class _$Quran extends Quran {
  _$Quran([ChopperClient? client]) {
    if (client == null) return;
    this.client = client;
  }

  @override
  final definitionType = Quran;

  @override
  Future<Response<AyatChunkDto>> quranGetAyatByPage(
      {int? startPage, int? endPage}) {
    final $url = '/api/Quran/GetAyatByPage';
    final $params = <String, dynamic>{
      'startPage': startPage,
      'endPage': endPage
    };
    final $request = Request('GET', $url, client.baseUrl, parameters: $params);
    return client.send<AyatChunkDto, AyatChunkDto>($request);
  }

  @override
  Future<Response<SurahChunkDto>> quranGetAyatBySurah(
      {int? surahId, int? startPage, int? endPage}) {
    final $url = '/api/Quran/GetAyatBySurah';
    final $params = <String, dynamic>{
      'surahId': surahId,
      'startPage': startPage,
      'endPage': endPage
    };
    final $request = Request('GET', $url, client.baseUrl, parameters: $params);
    return client.send<SurahChunkDto, SurahChunkDto>($request);
  }

  @override
  Future<Response<AyatChunkDto>> quranGetAyatByRub({int? rubId}) {
    final $url = '/api/Quran/GetAyatByRub';
    final $params = <String, dynamic>{'rubId': rubId};
    final $request = Request('GET', $url, client.baseUrl, parameters: $params);
    return client.send<AyatChunkDto, AyatChunkDto>($request);
  }

  @override
  Future<Response<AyatChunkDto>> quranGetAyatByJoz({int? jozId}) {
    final $url = '/api/Quran/GetAyatByJoz';
    final $params = <String, dynamic>{'jozId': jozId};
    final $request = Request('GET', $url, client.baseUrl, parameters: $params);
    return client.send<AyatChunkDto, AyatChunkDto>($request);
  }

  @override
  Future<Response<List<SurahDto>>> quranGetSurahList() {
    final $url = '/api/Quran/GetSurahList';
    final $request = Request('GET', $url, client.baseUrl);
    return client.send<List<SurahDto>, SurahDto>($request);
  }

  @override
  Future<Response<List<WeatherForecast>>> weatherForecastGet() {
    final $url = '/api/WeatherForecast';
    final $request = Request('GET', $url, client.baseUrl);
    return client.send<List<WeatherForecast>, WeatherForecast>($request);
  }
}
