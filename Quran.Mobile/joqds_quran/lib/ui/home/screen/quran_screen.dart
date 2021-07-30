import 'package:flutter/material.dart';
import 'package:joqds_quran/api/client_index.dart';
import 'package:joqds_quran/api/quran.swagger.dart';

class QuranScreen extends StatefulWidget {
  const QuranScreen({Key? key}) : super(key: key);

  @override
  State<QuranScreen> createState() => _QuranScreenState();
}

class _QuranScreenState extends State<QuranScreen> {
  @override
  void initState() {
    retriveSurahList();
    super.initState();
  }

  retriveSurahList() async {
    var result = await quranApi.quranGetSurahList();
    if (result.isSuccessful) {
      for (var item in result.body!) {
        setState(() {
          sovar.add(item);
        });
      }
    }
  }

  Quran quranApi = Quran.create();
  List<SurahDto> sovar = List<SurahDto>.empty(growable: true);

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      itemCount: sovar.length,
      itemBuilder: (context, index) {
        return Row(
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
        );
      },
    );
  }
}
