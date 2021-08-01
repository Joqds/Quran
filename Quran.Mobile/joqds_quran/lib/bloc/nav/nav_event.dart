import 'dart:async';
import 'package:flutter/cupertino.dart';
import 'package:joqds_quran/bloc/nav/index.dart';
import 'package:joqds_quran/bloc/nav/read_view_model.dart';
import 'package:meta/meta.dart';

@immutable
abstract class NavEvent {
  Stream<NavState> applyAsync(
      {required NavState currentState, required NavBloc bloc});
}

class GoHome extends NavEvent {
  @override
  Stream<NavState> applyAsync(
      {required NavState currentState, required NavBloc bloc}) async* {
    yield HomeNavState();
  }
}

class GoRead extends NavEvent {
  final int id;
  final ReadViewType type;
  final BuildContext context;

  GoRead({required this.id, required this.type, required this.context});
  @override
  Stream<NavState> applyAsync(
      {required NavState currentState, required NavBloc bloc}) async* {
    yield ReadNavState(ReadViewModel.type(type: type, id: id));
  }
}

// class UnNavEvent extends NavEvent {
//   @override
//   Stream<NavState> applyAsync({NavState? currentState, NavBloc? bloc}) async* {
//     yield const UnNavState();
//   }
// }

// class LoadNavEvent extends NavEvent {
   
//   @override
//   Stream<NavState> applyAsync(
//       {NavState? currentState, NavBloc? bloc}) async* {
//     try {
//       yield UnNavState();
//       await Future.delayed(const Duration(seconds: 1));
//       yield InNavState('Hello world');
//     } catch (_, stackTrace) {
//       developer.log('$_', name: 'LoadNavEvent', error: _, stackTrace: stackTrace);
//       yield ErrorNavState( _.toString());
//     }
//   }
// }
