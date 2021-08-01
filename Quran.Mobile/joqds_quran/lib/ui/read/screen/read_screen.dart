import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:joqds_quran/bloc/bloc.dart';
import 'package:joqds_quran/bloc/nav/index.dart';

import 'package:persian_number_utility/persian_number_utility.dart';

class ReadScreen extends StatelessWidget {
  const ReadScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return WillPopScope(onWillPop: () async {
      BlocProvider.of<NavBloc>(context).add(GoHome());
      return true;
    }, child: BlocBuilder<AyahBloc, AyahState>(builder: (context, state) {
      return Scaffold(
        persistentFooterButtons: [
          Row(
            children: [
              Expanded(
                child: BackButton(
                  onPressed: () {
                    BlocProvider.of<NavBloc>(context).add(GoHome());
                  },
                ),
              ),
              if (state is InAyahState)
                Expanded(
                  child: Center(
                      child: Text(
                    getBottomText(state),
                    style: Theme.of(context).textTheme.headline5,
                  )),
                  flex: 5,
                )
            ],
          )
        ],
        body: SafeArea(
          child: Builder(
            builder: (context) {
              if (state is UnAyahState) {
                return const Center(child: CircularProgressIndicator());
              }
              if (state is ErrorAyahState) {
                return Center(
                    child: Column(
                  crossAxisAlignment: CrossAxisAlignment.center,
                  children: [
                    Text("خطایی رخ داده (${state.errorMessage})"),
                    ElevatedButton(
                        onPressed: () {
                          BlocProvider.of<AyahBloc>(context)
                              .add(LoadAyahEvent(state.model));
                        },
                        child: const Text("تلاش مجدد"))
                  ],
                ));
              }
              if (state is InAyahState) {
                return ListView.separated(
                  itemCount: state.ayat.length,
                  itemBuilder: (context, index) {
                    var text =
                        "${state.ayat[index].text!} (${state.ayat[index].ayahInSurah.toString().toPersianDigit()})";
                    return Column(
                      children: [
                        if (state.ayat[index].ayahInSurah == 1 || index == 0)
                          Text(state.ayat[index].surahName!),
                        if (state.ayat[index].ayahInSurah == 1)
                          Text("بسم الله الرحمن الرحیم",
                              textAlign: TextAlign.center,
                              style: Theme.of(context).textTheme.bodyText1),
                        Text(text,
                            textAlign: TextAlign.center,
                            style: Theme.of(context)
                                .textTheme
                                .bodyText1!
                                .copyWith(fontSize: 30)),
                      ],
                    );
                  },
                  separatorBuilder: (context, index) {
                    return Column(
                      children: <Widget>[
                        if (index > 1 &&
                            state.ayat[index].pageId !=
                                state.ayat[index - 1].pageId)
                          Text("-- صفحه ${state.ayat[index].pageId} --"),
                        // if (index > 1 &&
                        //     state.ayat[index].surahId !=
                        //         state.ayat[index - 1].surahId)
                        //   Text("-- سوره ${state.ayat[index].surahName} --"),
                        if (index > 1 &&
                            state.ayat[index].rubId !=
                                state.ayat[index - 1].rubId)
                          Text(
                              "-- جزء ${state.ayat[index].rubJoz.toString().toPersianDigit()} - حزب ${((state.ayat[index].rubRubInJoz! + 1) / 2).floor().toString().toPersianDigit()} - ربع ${state.ayat[index].rubRubInJoz.toString().toPersianDigit()} --")
                      ],
                    );
                  },
                );
              }
              throw Exception();
            },
          ),
        ),
      );
    }));
  }

  String getBottomText(InAyahState state) {
    switch (state.model.type!) {
      case ReadViewType.surah:
        return "سوره ${state.ayat.first.surahName}";
      case ReadViewType.page:
        return "صفحه ${state.ayat.first.pageId.toString().toPersianDigit()}";
      case ReadViewType.joz:
        return "جزء ${state.ayat.first.rubJoz.toString().toPersianDigit()}";
      case ReadViewType.hizb:
        return "حزب ${((state.ayat.first.rubRubInJoz! / 2) + 1).floor().toString().toPersianDigit()} - جزء ${state.ayat.first.rubJoz.toString().toPersianDigit()}";
      case ReadViewType.rub:
        return "ربع ${state.ayat.first.rubRubInJoz.toString().toPersianDigit()} - حزب ${((state.ayat.first.rubRubInJoz! / 2) + 1).floor().toString().toPersianDigit()} - جزء ${state.ayat.first.rubJoz.toString().toPersianDigit()}";
    }
  }
}
