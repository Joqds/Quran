import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:joqds_quran/bloc/bloc.dart';
import 'package:joqds_quran/bloc/nav/read_view_model.dart';

import 'screen/read_screen.dart';

class ReadPage extends Page {
  const ReadPage(this.model) : super(key: const ValueKey(ReadPage));
  final ReadViewModel model;
  @override
  Route createRoute(BuildContext context) {
    return MaterialPageRoute(
      settings: this,
      builder: (context) {
        return Directionality(
            child: BlocProvider(
              create: (context) =>
                  AyahBloc(UnAyahState(model))..add(LoadAyahEvent(model)),
              child: const ReadScreen(),
            ),
            textDirection: TextDirection.rtl);
      },
    );
  }
}
