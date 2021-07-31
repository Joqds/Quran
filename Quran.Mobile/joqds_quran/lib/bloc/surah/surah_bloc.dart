import 'dart:async';
import 'dart:developer' as developer;

import 'package:bloc/bloc.dart';
import 'surah.dart';

class SurahBloc extends Bloc<SurahEvent, SurahState> {
  SurahBloc(SurahState initialState) : super(initialState);

  @override
  Stream<SurahState> mapEventToState(
    SurahEvent event,
  ) async* {
    try {
      yield* event.applyAsync(currentState: state, bloc: this);
    } catch (_, stackTrace) {
      developer.log('$_', name: 'SurahBloc', error: _, stackTrace: stackTrace);
      yield state;
    }
  }
}
