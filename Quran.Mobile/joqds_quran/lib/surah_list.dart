import 'package:flutter/material.dart';
import 'package:joqds_quran/api/quran.swagger.dart';

class SurahList extends StatefulWidget {
  const SurahList({Key? key}) : super(key: key);

  @override
  _SurahListState createState() => _SurahListState();
}

class _SurahListState extends State<SurahList> {
  _SurahListState() {
    quranApi = Quran.create();
    sovar = List.empty(growable: true);
  }

  late Quran quranApi;
  late List<SurahDto> sovar;
  @override
  void initState() {
    retriveSurahList();

    super.initState();
  }

  retriveSurahList() async {
    var result = await quranApi.quranGetSurahList();
    for (var item in result.body!) {
      setState(() {
        sovar.add(item);
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemBuilder: (context, index) {
        return MaterialButton(
          onPressed: () {
            final snackBar = SnackBar(
                content: Text(
                    'این سوره شامل ${sovar[index].ayatCount} آیه می باشد'));

            // Find the ScaffoldMessenger in the widget tree
            // and use it to show a SnackBar.
            var snackbarManager = ScaffoldMessenger.of(context);
            snackbarManager.clearSnackBars();
            snackbarManager.showSnackBar(snackBar);
          },
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              Expanded(
                flex: 1,
                child: Text(
                  sovar[index].id.toString(),
                  textAlign: TextAlign.center,
                  style: Theme.of(context).textTheme.headline5,
                ),
              ),
              Expanded(
                flex: 5,
                child: Text(sovar[index].name!,
                    textAlign: TextAlign.center,
                    style: Theme.of(context).textTheme.headline5!),
              ),
            ],
          ),
        );
      },
      itemCount: sovar.length,
    );
  }
}
