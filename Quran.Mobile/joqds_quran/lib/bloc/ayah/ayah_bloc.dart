import 'dart:async';
import 'dart:developer' as developer;

import 'package:bloc/bloc.dart';

import 'ayah.dart';

class AyahBloc extends Bloc<AyahEvent, AyahState> {
  AyahBloc(AyahState initialState) : super(initialState);

  @override
  Stream<AyahState> mapEventToState(
    AyahEvent event,
  ) async* {
    try {
      yield* event.applyAsync(
          currentState: state, bloc: this, model: state.model);
    } catch (_, stackTrace) {
      developer.log('$_', name: 'AyahBloc', error: _, stackTrace: stackTrace);
      yield state;
    }
  }
}
