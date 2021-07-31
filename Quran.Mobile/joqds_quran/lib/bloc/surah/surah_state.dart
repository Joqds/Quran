import 'package:equatable/equatable.dart';
import 'package:joqds_quran/api/quran.swagger.dart';

abstract class SurahState extends Equatable {
  const SurahState();
  @override
  List<Object> get props => [];
}

/// UnInitialized
class UnSurahState extends SurahState {
  const UnSurahState();

  @override
  String toString() => 'UnSurahState';
}

/// Initialized
class InSurahState extends SurahState {
  const InSurahState({required this.sovar});
  final List<SurahDto> sovar;

  @override
  List<Object> get props => [sovar.length];
}

class ErrorSurahState extends SurahState {
  const ErrorSurahState(this.errorMessage);

  final String errorMessage;

  @override
  String toString() => 'ErrorSurahState';

  @override
  List<Object> get props => [errorMessage];
}
