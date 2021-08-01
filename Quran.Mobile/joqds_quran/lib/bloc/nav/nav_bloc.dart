import 'dart:async';
import 'dart:developer' as developer;

import 'package:bloc/bloc.dart';
import 'package:joqds_quran/bloc/nav/index.dart';

class NavBloc extends Bloc<NavEvent, NavState> {
  NavBloc() : super(HomeNavState());

  @override
  Stream<NavState> mapEventToState(
    NavEvent event,
  ) async* {
    try {
      yield* event.applyAsync(currentState: state, bloc: this);
    } catch (_, stackTrace) {
      developer.log('$_', name: 'NavBloc', error: _, stackTrace: stackTrace);
      yield state;
    }
  }
}
