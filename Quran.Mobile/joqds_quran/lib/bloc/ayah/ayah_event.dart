import 'dart:async';
import 'dart:developer' as developer;

import 'package:joqds_quran/api/client_index.dart';

import 'ayah.dart';

import 'package:meta/meta.dart';

@immutable
abstract class AyahEvent {
  Stream<AyahState> applyAsync({AyahState currentState, AyahBloc bloc});
}

class UnAyahEvent extends AyahEvent {
  @override
  Stream<AyahState> applyAsync(
      {AyahState? currentState, AyahBloc? bloc}) async* {
    yield UnAyahState();
  }
}

class LoadAyahBySurahEvent extends AyahEvent {
  final int surahId;

  LoadAyahBySurahEvent(this.surahId);
  @override
  Stream<AyahState> applyAsync(
      {AyahState? currentState, AyahBloc? bloc}) async* {
    try {
      yield UnAyahState();
      var api = Quran.create();
      var response = await api.quranGetAyatBySurah(surahId: surahId);
      if (response.isSuccessful) {
        yield InAyahState(response.body!.ayat!);
      } else {
        yield ErrorAyahState(response.statusCode.toString());
      }
    } catch (_, stackTrace) {
      developer.log('$_',
          name: 'LoadAyahEvent', error: _, stackTrace: stackTrace);
      yield ErrorAyahState(_.toString());
    }
  }
}
