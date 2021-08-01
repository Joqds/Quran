import 'dart:async';
import 'dart:developer' as developer;

import 'package:joqds_quran/api/client_index.dart';
import 'package:joqds_quran/api/quran.swagger.dart';

import 'ayah.dart';

import 'package:meta/meta.dart';

@immutable
abstract class AyahEvent {
  Stream<AyahState> applyAsync(
      {AyahState currentState, AyahBloc bloc, required int surahId});
}

class UnAyahEvent extends AyahEvent {
  @override
  Stream<AyahState> applyAsync(
      {AyahState? currentState, AyahBloc? bloc, required int surahId}) async* {
    yield UnAyahState(surahId);
  }
}

class LoadAyahBySurahEvent extends AyahEvent {
  final int surahId;

  LoadAyahBySurahEvent(this.surahId);
  @override
  Stream<AyahState> applyAsync(
      {AyahState? currentState, AyahBloc? bloc, required int surahId}) async* {
    try {
      yield UnAyahState(surahId);
      var api = Quran.create();
      var response = await api.quranGetAyatBySurah(surahId: surahId);
      if (response.isSuccessful) {
        yield InAyahState(response.body!.ayat!, surahId);
      } else {
        yield ErrorAyahState(response.statusCode.toString(), surahId);
      }
    } catch (_, stackTrace) {
      developer.log('$_',
          name: 'LoadAyahEvent', error: _, stackTrace: stackTrace);
      yield ErrorAyahState(_.toString(), surahId);
    }
  }
}
