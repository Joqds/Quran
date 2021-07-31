import 'package:equatable/equatable.dart';
import 'package:joqds_quran/api/quran.swagger.dart';

abstract class AyahState extends Equatable {
  AyahState();

  @override
  List<Object> get props => [];
}

/// UnInitialized
class UnAyahState extends AyahState {
  UnAyahState();

  @override
  String toString() => 'UnAyahState';
}

/// Initialized
class InAyahState extends AyahState {
  InAyahState(this.ayat);

  final List<AyahDto> ayat;

  @override
  List<Object> get props => [
        ayat.fold<int>(
            0, (previousValue, element) => previousValue + element.id!)
      ];
}

class ErrorAyahState extends AyahState {
  ErrorAyahState(this.errorMessage);

  final String errorMessage;

  @override
  String toString() => 'ErrorAyahState';

  @override
  List<Object> get props => [errorMessage];
}
