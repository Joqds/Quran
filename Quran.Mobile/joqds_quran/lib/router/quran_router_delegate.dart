import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:joqds_quran/bloc/nav/index.dart';

import 'quran_path.dart';

class QuranRouterDelegate extends RouterDelegate<QuranPathInformation>
    with ChangeNotifier, PopNavigatorRouterDelegateMixin<QuranPathInformation> {
  final GlobalKey<NavigatorState> _navigatorKey = GlobalKey<NavigatorState>();

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<NavBloc, NavState>(
      builder: (context, state) {
        if (state is PageLoadingState) return Container();
        return Navigator(
          key: _navigatorKey,
          pages: state.pages,
          onPopPage: (route, result) {
            return route.didPop(result);
          },
        );
      },
    );
  }

  @override
  GlobalKey<NavigatorState>? get navigatorKey => _navigatorKey;

  @override
  Future<void> setNewRoutePath(QuranPathInformation configuration) async {
    /*do nothing!!!*/
  }
}
