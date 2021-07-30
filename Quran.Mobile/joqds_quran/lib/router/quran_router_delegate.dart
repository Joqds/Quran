import 'package:flutter/material.dart';
import 'package:joqds_quran/ui/home/home_page.dart';

import 'quran_path.dart';

class QuranRouterDelegate extends RouterDelegate<QuranPathInformation>
    with ChangeNotifier, PopNavigatorRouterDelegateMixin<QuranPathInformation> {
  final GlobalKey<NavigatorState> _navigatorKey = GlobalKey<NavigatorState>();

  @override
  Widget build(BuildContext context) {
    return Navigator(
      key: _navigatorKey,
      pages: const [HomePage()],
      onPopPage: (route, result) {
        return route.didPop(result);
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
