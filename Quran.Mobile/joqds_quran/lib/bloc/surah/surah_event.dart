import 'dart:async';

import 'package:joqds_quran/api/quran.swagger.dart';
import 'package:meta/meta.dart';

import 'surah.dart';

@immutable
abstract class SurahEvent {
  Stream<SurahState> applyAsync({SurahState currentState, SurahBloc bloc});
}

class LoadSurahEvent extends SurahEvent {
  @override
  Stream<SurahState> applyAsync(
      {SurahState? currentState, SurahBloc? bloc}) async* {
    try {
      yield const UnSurahState();

      var api = Quran.create();
      var response = await api.quranGetSurahList();
      if (response.isSuccessful) {
        yield InSurahState(sovar: response.body!);
      } else {
        yield ErrorSurahState(response.statusCode.toString());
      }
    } catch (e) {
      yield ErrorSurahState(e.toString());
    }
  }
}
