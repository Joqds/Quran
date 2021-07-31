import 'package:equatable/equatable.dart';
import 'package:joqds_quran/api/quran.swagger.dart';

abstract class AyahState extends Equatable {
  const AyahState(this.surahId);
  final int surahId;
  @override
  List<Object> get props => [surahId];
}

/// UnInitialized
class UnAyahState extends AyahState {
  const UnAyahState(int surahId) : super(surahId);

  @override
  String toString() => 'UnAyahState';
}

/// Initialized
class InAyahState extends AyahState {
  const InAyahState(this.ayat, int surahId) : super(surahId);

  final List<AyahDto> ayat;
}

class ErrorAyahState extends AyahState {
  const ErrorAyahState(this.errorMessage, int surahId) : super(surahId);

  final String errorMessage;

  @override
  String toString() => 'ErrorAyahState';
}
