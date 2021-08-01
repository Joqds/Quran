import 'package:equatable/equatable.dart';
import 'package:flutter/material.dart';
import 'package:joqds_quran/bloc/nav/read_view_model.dart';
import 'package:joqds_quran/ui/home/home_page.dart';
import 'package:joqds_quran/ui/read/read_page.dart';

abstract class NavState extends Equatable {
  const NavState();
  String get path;
  List<Page<dynamic>> get pages;
  @override
  List<Object> get props => [];
}

class PageLoadingState extends NavState {
  @override
  String get path => "/loading";

  @override
  List<Page> get pages => [];
}

class HomeNavState extends NavState {
  @override
  String get path => "/";

  @override
  List<Page> get pages => [const HomePage()];
}

class ReadNavState extends NavState {
  final ReadViewModel model;

  const ReadNavState(this.model);

  @override
  String get path => "/read/surah/${model.surahId}";

  @override
  List<Page> get pages => [const HomePage(), ReadPage(model)];
}

// // UnInitialized
// class UnNavState extends NavState {
//   const UnNavState();

//   @override
//   String toString() => 'UnNavState';
// }

// /// Initialized
// class InNavState extends NavState {
//   const InNavState(this.hello);

//   final String hello;

//   @override
//   String toString() => 'InNavState $hello';

//   @override
//   List<Object> get props => [hello];
// }

// class ErrorNavState extends NavState {
//   const ErrorNavState(this.errorMessage);

//   final String errorMessage;

//   @override
//   String toString() => 'ErrorNavState';

//   @override
//   List<Object> get props => [errorMessage];
// }
