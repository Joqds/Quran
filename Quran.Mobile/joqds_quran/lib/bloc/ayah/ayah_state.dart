import 'package:equatable/equatable.dart';
import 'package:joqds_quran/api/quran.swagger.dart';
import 'package:joqds_quran/bloc/bloc.dart';

abstract class AyahState extends Equatable {
  const AyahState(this.model);
  final ReadViewModel model;
  @override
  List<Object> get props => [model.id];
}

/// UnInitialized
class UnAyahState extends AyahState {
  const UnAyahState(ReadViewModel model) : super(model);

  @override
  String toString() => 'UnAyahState';
}

/// Initialized
class InAyahState extends AyahState {
  const InAyahState(this.ayat, ReadViewModel model) : super(model);

  final List<AyahDto> ayat;
}

class ErrorAyahState extends AyahState {
  const ErrorAyahState(this.errorMessage, ReadViewModel model) : super(model);

  final String errorMessage;

  @override
  String toString() => 'ErrorAyahState';
}
