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
        floatingActionButtonLocation: FloatingActionButtonLocation.startFloat,
        floatingActionButton: FloatingActionButton(
          mini: true,
          child: const BackButton(),
          onPressed: () {
            BlocProvider.of<NavBloc>(context).add(GoHome());
          },
        ),
        // persistentFooterButtons: [
        //   Row(
        //     crossAxisAlignment: CrossAxisAlignment.center,
        //     children: [
        //       Expanded(
        //         child: BackButton(
        //           onPressed: () {
        //             BlocProvider.of<NavBloc>(context).add(GoHome());
        //           },
        //         ),
        //       ),
        //       if (state is InAyahState)
        //         Expanded(
        //           child: Center(
        //               child: Text(
        //             getBottomText(state),
        //             style: Theme.of(context).textTheme.headline5,
        //           )),
        //           flex: 5,
        //         )
        //     ],
        //   )
        // ],
        body: SafeArea(
          child: Builder(
            builder: (context) {
              if (state is UnAyahState) {
                return const Center(child: CircularProgressIndicator());
              }
              if (state is ErrorAyahState) {
                return Center(
                    child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
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
                  padding: const EdgeInsets.only(bottom: 50),
                  itemCount: state.ayat.length,
                  itemBuilder: (context, index) {
                    var text = state.ayat[index].text!;
                    return Padding(
                      padding: const EdgeInsets.only(bottom: 24.0),
                      child: Column(
                        children: [
                          if (state.ayat[index].ayahInSurah == 1 || index == 0)
                            Padding(
                              padding: const EdgeInsets.only(
                                  bottom: 24.0, top: 24.0),
                              child: Row(
                                crossAxisAlignment: CrossAxisAlignment.center,
                                mainAxisAlignment: MainAxisAlignment.center,
                                children: [
                                  const Icon(Icons.flare),
                                  Padding(
                                    padding: const EdgeInsets.only(
                                        left: 10, right: 10),
                                    child: Text(
                                      state.ayat[index].surahName!,
                                      style: Theme.of(context)
                                          .textTheme
                                          .headline3!,
                                    ),
                                  ),
                                  const Icon(Icons.flare),
                                ],
                              ),
                            ),
                          if (state.ayat[index].ayahInSurah == 1 &&
                              state.ayat[index].surahId != 1 &&
                              state.ayat[index].surahId != 9)
                            Padding(
                              padding: const EdgeInsets.only(bottom: 24.0),
                              child: Text(
                                  "بِسْمِ ٱللَّهِ ٱلرَّحْمَٰنِ ٱلرَّحِيمِ",
                                  textAlign: TextAlign.center,
                                  style:
                                      Theme.of(context).textTheme.headline5!),
                            ),
                          Text(
                              "$text ﴿${state.ayat[index].ayahInSurah.toString().toPersianDigit()}﴾",
                              textAlign: TextAlign.center,
                              style: Theme.of(context).textTheme.headline5!),
                          // Text(
                          //     "(${state.ayat[index].ayahInSurah.toString().toPersianDigit()})",
                          //     textAlign: TextAlign.center,
                          //     style: Theme.of(context).textTheme.headline5!),
                        ],
                      ),
                    );
                  },
                  separatorBuilder: (context, index) {
                    return Column(
                      children: <Widget>[
                        if (index > 1 &&
                            state.ayat[index].pageId !=
                                state.ayat[index - 1].pageId)
                          Text(
                            "۞ صفحه ${state.ayat[index].pageId.toString().toPersianDigit()} ۞",
                            style: Theme.of(context).textTheme.subtitle1,
                          ),
                        // if (index > 1 &&
                        //     state.ayat[index].surahId !=
                        //         state.ayat[index - 1].surahId)
                        //   Text("-- سوره ${state.ayat[index].surahName} --"),
                        if (index > 1 &&
                            state.ayat[index].rubId !=
                                state.ayat[index - 1].rubId)
                          Text(
                            "۞ جزء ${state.ayat[index].rubJoz.toString().toPersianDigit()} ۞ حزب ${((state.ayat[index].rubRubInJoz! + 1) / 2).floor().toString().toPersianDigit()} ۞ ربع ${state.ayat[index].rubRubInJoz.toString().toPersianDigit()} ۞",
                            style: Theme.of(context).textTheme.subtitle1,
                          )
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
        return "حزب ${((state.ayat.first.rubRubInJoz! / 2) + 1).floor().toString().toPersianDigit()} ۞ جزء ${state.ayat.first.rubJoz.toString().toPersianDigit()}";
      case ReadViewType.rub:
        return "ربع ${state.ayat.first.rubRubInJoz.toString().toPersianDigit()} ۞ حزب ${((state.ayat.first.rubRubInJoz! / 2) + 1).floor().toString().toPersianDigit()} ۞ جزء ${state.ayat.first.rubJoz.toString().toPersianDigit()}";
    }
  }
}
