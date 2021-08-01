import 'dart:async';
import 'dart:developer' as developer;

import 'package:joqds_quran/api/client_index.dart';
import 'package:joqds_quran/api/quran.swagger.dart';
import 'package:joqds_quran/bloc/bloc.dart';

import 'ayah.dart';

import 'package:meta/meta.dart';

@immutable
abstract class AyahEvent {
  Stream<AyahState> applyAsync(
      {AyahState currentState, AyahBloc bloc, required ReadViewModel model});
}

class UnAyahEvent extends AyahEvent {
  @override
  Stream<AyahState> applyAsync(
      {AyahState? currentState,
      AyahBloc? bloc,
      required ReadViewModel model}) async* {
    yield UnAyahState(model);
  }
}

class LoadAyahEvent extends AyahEvent {
  final ReadViewModel model;

  LoadAyahEvent(this.model);
  @override
  Stream<AyahState> applyAsync(
      {AyahState? currentState,
      AyahBloc? bloc,
      required ReadViewModel model}) async* {
    try {
      yield UnAyahState(model);
      var api = Quran.create();
      //var response = await api.quranGetAyatBySurah(surahId: surahId);
      switch (model.type) {
        case ReadViewType.surah:
          var response = await api.quranGetAyatBySurah(surahId: model.id);
          if (response.isSuccessful) {
            yield InAyahState(response.body!.ayat!, model);
          } else {
            yield ErrorAyahState(response.statusCode.toString(), model);
          }
          break;
        case ReadViewType.page:
          var response = await api.quranGetAyatByPage(startPage: model.id);
          if (response.isSuccessful) {
            yield InAyahState(response.body!.ayat!, model);
          } else {
            yield ErrorAyahState(response.statusCode.toString(), model);
          }
          break;
        case ReadViewType.joz:
          var response = await api.quranGetAyatByJoz(jozId: model.id);
          if (response.isSuccessful) {
            yield InAyahState(response.body!.ayat!, model);
          } else {
            yield ErrorAyahState(response.statusCode.toString(), model);
          }
          break;
        case ReadViewType.hizb:
          var response = await api.quranGetAyatByRub(rubId: model.id * 2 - 1);
          if (response.isSuccessful) {
            yield InAyahState(response.body!.ayat!, model);
          } else {
            yield ErrorAyahState(response.statusCode.toString(), model);
          }
          break;
        case ReadViewType.rub:
          var response = await api.quranGetAyatByRub(rubId: model.id);
          if (response.isSuccessful) {
            yield InAyahState(response.body!.ayat!, model);
          } else {
            yield ErrorAyahState(response.statusCode.toString(), model);
          }
          break;
        default:
          yield ErrorAyahState("خطای غیر منتظره", model);
      }
    } catch (_, stackTrace) {
      developer.log('$_',
          name: 'LoadAyahEvent', error: _, stackTrace: stackTrace);
      yield ErrorAyahState(_.toString(), model);
    }
  }
}
